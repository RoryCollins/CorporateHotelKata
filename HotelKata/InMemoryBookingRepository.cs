using System;
using System.Collections.Generic;

namespace HotelKata
{
    public partial class InMemoryBookingRepository : BookingRepository
    {
        private readonly List<Booking> bookings = new List<Booking>();
        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public IEnumerable<Booking> GetBookings(Guid hotelId, RoomType roomType)
        {
            return bookings;
        }
    }
}