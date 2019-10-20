using System;
using Xunit;

namespace HotelKata.test.Unit
{
    public class InMemoryBookingRepositoryShould
    {
        [Fact]
        public void AddAndRetrieveBooking()
        {
            var bookingRepository = new InMemoryBookingRepository();

            var hotelId = Guid.NewGuid();
            var booking = new Booking(Guid.NewGuid(), hotelId, RoomType.Standard, "12/10/2019", "19/10/2019");
            
            bookingRepository.AddBooking(booking);
            Assert.Contains(booking, bookingRepository.GetBookings(hotelId, RoomType.Standard));
        }
        
//        [Fact]
//        public void 
    }
}