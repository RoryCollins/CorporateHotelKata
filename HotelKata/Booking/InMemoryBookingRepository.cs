using System;
using System.Collections.Generic;
using System.Linq;
using HotelKata.Room;

namespace HotelKata.Booking
{
    public class InMemoryBookingRepository : BookingRepository
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
        public IEnumerable<Booking> GetActiveBookings(Guid hotelId, RoomType roomType, DateTime currentDate)
        {
            return bookings.Where(it=>it.CheckIn <= currentDate && it.CheckOut > currentDate );
        }
    }
}