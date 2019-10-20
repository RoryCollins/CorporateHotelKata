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

namespace HotelKata.test.Unit
{
    public class BookingServiceShould
    {
        private readonly Guid hotelId = Guid.NewGuid();
        private readonly DateTime checkIn = DateTime.Parse("12/10/2019");
        private readonly DateTime checkOut = DateTime.Parse("12/11/2019"); 
        private readonly Guid employeeId = Guid.NewGuid();
        private readonly Mock<HotelService> mockHotelService = new Mock<HotelService>();
        private readonly Mock<BookingRepository> bookingRepository= new Mock<BookingRepository>();
        private readonly Mock<BookingPolicyService> mockBookingPolicyService  = new Mock<BookingPolicyService>();
        private readonly Mock<IdGenerator> idGenerator = new Mock<IdGenerator>();
        private readonly BookingService bookingService;

        public BookingServiceShould()
        {
            bookingService = new BookingService(mockHotelService.Object, bookingRepository.Object, 
                mockBookingPolicyService.Object, idGenerator.Object);
            var hotel = new Hotel.Hotel(hotelId, "Hotel Marigold");
            hotel.SetRoom(101, Standard);
            mockHotelService.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);
            mockBookingPolicyService.Setup(it => it.isBookingAllowed(employeeId, Standard)).Returns(true);

        }

        [Fact]
        public void CreateBookingIfRoomIsAvailable()
        {
            var booking = bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            Assert.NotNull(booking);
        }

        [Fact]
        public void ThrowExceptionIfRoomDoesNotExist()
        {
            Assert.Throws<RoomUnavailable>(() =>
                bookingService.Book(employeeId, hotelId, Master, checkIn, checkOut));
        }

        [Fact]
        public void StoreABooking()
        {
            var bookingId = Guid.NewGuid();
            idGenerator.Setup(it => it.GenerateId()).Returns(bookingId);
            bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            var booking = aBooking()
                            .WithId(bookingId)
                            .WithEmployeeId(employeeId)
                            .WithHotelId(hotelId)
                            .From(checkIn)
                            .To(checkOut)
                            .Build();
            bookingRepository.Verify(it=>it.AddBooking(booking));
        }

        [Fact]
        public void ThrowExceptionIfRoomIsAlreadyBooked()
        {
            var existingBooking = aBooking().WithEmployeeId(employeeId).WithHotelId(hotelId).Build();
            
            bookingRepository.Setup(it => it.GetActiveBookings(hotelId, Standard, checkIn)).Returns(new List<Booking.Booking> {existingBooking});
            Assert.Throws<RoomUnavailable>(() => bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut));
        }

        [Fact]
        public void ThrowExceptionIfAgainstBookingPolicy()
        {
            var hotel = new Hotel.Hotel(hotelId, "Hotel Marigold");
            hotel.SetRoom(101, Master);
            mockHotelService.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);
            mockBookingPolicyService.Setup(it => it.isBookingAllowed(employeeId, Master)).Returns(false);
            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, RoomType.Master, checkIn, checkOut));
        }

        [Fact]
        public void ThrowExceptionIfCheckOutIsNotAtLeastOneDayAfterCheckIn()
        {
            Assert.Throws<CheckoutDateInvalid>(() =>
                bookingService.Book(employeeId, hotelId, RoomType.Master, checkIn, checkIn));
        }
    }
}

