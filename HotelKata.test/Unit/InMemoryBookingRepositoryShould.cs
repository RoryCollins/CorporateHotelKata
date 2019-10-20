using System;
using Xunit;
using static HotelKata.RoomType;

namespace HotelKata.test.Unit
{
    public class InMemoryBookingRepositoryShould
    {
        [Fact]
        public void AddAndRetrieveBooking()
        {
            var bookingRepository = new InMemoryBookingRepository();
            var hotelId = Guid.NewGuid();
            var booking = BookingBuilder.aBooking().WithHotelId(hotelId).WithRoomType(Standard).Build();
            
            bookingRepository.AddBooking(booking);
            Assert.Contains(booking, bookingRepository.GetBookings(hotelId, Standard));
        }
        
    }
}