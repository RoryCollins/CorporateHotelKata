using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Moq;
using Xunit;
using static HotelKata.RoomType;
using static HotelKata.test.Unit.BookingBuilder;

namespace HotelKata.test.Unit
{
    public class BookingServiceShould
    {
        private readonly Guid hotelId;
        private readonly string checkIn;
        private readonly string checkOut;
        private readonly BookingService bookingService;
        private readonly Guid employeeId;
        private readonly Mock<HotelService> mockHotelService;
        private readonly Mock<BookingRepository> bookingRepository;
        private readonly Mock<BookingPolicyService> mockBookingPolicyService;

        public BookingServiceShould()
        {
            mockBookingPolicyService = new Mock<BookingPolicyService>();
            mockHotelService = new Mock<HotelService>();
            bookingRepository = new Mock<BookingRepository>();
            bookingService = new BookingService(mockHotelService.Object, bookingRepository.Object, mockBookingPolicyService.Object);
            employeeId = Guid.NewGuid();
            hotelId = Guid.NewGuid();
            checkIn = "12/10/2019";
            checkOut = "12/11/2019"; 
            var hotel = new Hotel(hotelId, "Hotel Marigold");
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
            
            bookingRepository.Setup(it => it.GetBookings(hotelId, Standard)).Returns(new List<Booking> {booking});
            Assert.Throws<RoomUnavailable>(() => bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut));
        }

        [Fact]
        public void ThrowExceptionIfAgainstBookingPolicy()
        {
            var hotel = new Hotel(hotelId, "Hotel Marigold");
            hotel.SetRoom(101, Master);
            mockHotelService.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);
            mockBookingPolicyService.Setup(it => it.isBookingAllowed(employeeId, Master)).Returns(false);
            Assert.Throws<InsufficientPrivilege>(() =>
                bookingService.Book(employeeId, hotelId, RoomType.Master, checkIn, checkOut));
        }
    }
}

