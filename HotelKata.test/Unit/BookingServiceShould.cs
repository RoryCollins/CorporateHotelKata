using System;
using Moq;
using Xunit;
using static HotelKata.RoomType;

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

        public BookingServiceShould()
        {
            mockHotelService = new Mock<HotelService>();
            bookingService = new BookingService(mockHotelService.Object);
            employeeId = Guid.NewGuid();
            hotelId = Guid.NewGuid();
            checkIn = "12/10/2019";
            checkOut = "12/11/2019"; 
        }

        [Fact]
        public void CreateBookingIfRoomIsAvailable()
        {
            var hotel = new Hotel(hotelId, "Hotel Marigold");
            hotel.SetRoom(101, Standard);
            mockHotelService.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);

            var booking = bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            Assert.NotNull(booking);
        }

        [Fact]
        public void ThrowExceptionIfRoomIsUnavailable()
        {
            var hotel = new Hotel(hotelId, "Hotel Marigold");
            hotel.SetRoom(101, Standard);
            mockHotelService.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);
            
            Assert.Throws<RoomUnavailable>(() =>
                bookingService.Book(employeeId, hotelId, Master, checkIn, checkOut));
        }
    }
}