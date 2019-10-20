using System;
using System.Collections.Generic;
using HotelKata.Booking;
using HotelKata.BookingPolicy;
using HotelKata.Hotel;
using HotelKata.Room;
using Moq;
using Xunit;
using static HotelKata.Room.RoomType;
using static HotelKata.test.Unit.BookingBuilder;

namespace HotelKata.test.Acceptance
{
    public class BookASingleRoomFeature
    {
        private readonly ProductionHotelService hotelService;
        private BookingRepository bookingRepository;
        private BookingPolicyService bookingPolicyService;
        private readonly Guid employeeId;
        private readonly DateTime checkIn = DateTime.Parse("12/10/2019");
        private readonly DateTime checkOut = DateTime.Parse("19/10/2019");
        private readonly BookingService bookingService;
        private readonly Mock<IdGenerator> idGenerator = new Mock<IdGenerator>();

        public BookASingleRoomFeature()
        {
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository);
            HotelRepository hotelRepository = new InMemoryHotelRepository();
            hotelService = new ProductionHotelService(hotelRepository);
            bookingRepository = new InMemoryBookingRepository();
            employeeId = Guid.NewGuid();
            bookingService = new BookingService(hotelService, bookingRepository, bookingPolicyService, idGenerator.Object);
        }

        [Fact]
        public void TheOneWhereISuccessfullyBookASingleRoom()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);
            var bookingId = Guid.NewGuid();
            idGenerator.Setup(it => it.GenerateId()).Returns(bookingId);

            var expectedBooking = aBooking()
                                    .WithId(bookingId)
                                    .WithEmployeeId(employeeId)
                                    .WithHotelId(hotelId)
                                    .From(checkIn)
                                    .To(checkOut)
                                    .Build();
            var actualBooking = bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            
            Assert.Equal(expectedBooking, actualBooking);
        }

        [Fact]
        public void TheOneWhereThereAreNoRoomsAvailable()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);
            
            bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);

            Assert.Throws<RoomUnavailable>(() =>bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut));
        }

        [Fact]
        public void TheOneWhereTheEmployeeDoesNotHaveSufficientPrivilege()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);
            hotelService.SetRoom(hotelId, 501, Master);
            
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType>() {Standard});

            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, Master, checkIn, checkOut));
        }

        [Fact]
        public void TheOneWhereIMustCheckOutAtLeastOneDayAfterCheckingIn()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);
            
            Assert.Throws<CheckoutDateInvalid>(() =>
                bookingService.Book(employeeId, hotelId, Master, DateTime.Parse("01/04/2018"), DateTime.Parse("01/04/2018")));
        }
    }
}