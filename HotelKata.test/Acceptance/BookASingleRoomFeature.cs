using System;
using System.Collections.Generic;
using HotelKata.Booking;
using HotelKata.BookingPolicy;
using HotelKata.Hotel;
using HotelKata.Room;
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
        private readonly string checkIn = "12/10/2019";
        private readonly string checkOut = "19/10/2019";
        private readonly BookingService bookingService;

        public BookASingleRoomFeature()
        {
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository);
            HotelRepository hotelRepository = new InMemoryHotelRepository();
            hotelService = new ProductionHotelService(hotelRepository);
            bookingRepository = new InMemoryBookingRepository();
            employeeId = Guid.NewGuid();
            bookingService = new BookingService(hotelService, bookingRepository, bookingPolicyService);
        }

        [Fact]
        public void TheOneWhereISuccessfullyBookASingleRoom()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);

            var expectedBooking = aBooking()
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
            
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType>(){Standard});

            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, Master, checkIn, checkOut));
        }
    }
}