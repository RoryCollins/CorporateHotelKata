using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        private readonly string checkIn = "12/10/2019";
        private readonly string checkOut = "12/11/2019"; 
        private readonly Guid employeeId = Guid.NewGuid();
        private readonly Mock<HotelService> mockHotelService = new Mock<HotelService>();
        private readonly Mock<BookingRepository> bookingRepository= new Mock<BookingRepository>();
        private readonly Mock<BookingPolicyService> mockBookingPolicyService  = new Mock<BookingPolicyService>();
        private readonly BookingService bookingService;

        public BookingServiceShould()
        {
            bookingService = new BookingService(mockHotelService.Object, bookingRepository.Object, mockBookingPolicyService.Object);
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
            bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            var booking = aBooking().WithEmployeeId(employeeId).WithHotelId(hotelId).Build();
            bookingRepository.Verify(it=>it.AddBooking(booking));
        }

        [Fact]
        public void ThrowExceptionIfRoomIsAlreadyBooked()
        {
            var booking = aBooking().WithEmployeeId(employeeId).WithHotelId(hotelId).Build();
            
            bookingRepository.Setup(it => it.GetBookings(hotelId, Standard)).Returns(new List<Booking.Booking> {booking});
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
    }
}

