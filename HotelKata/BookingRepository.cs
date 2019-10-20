using System;
using System.Collections.Generic;

namespace HotelKata
{
    public interface BookingRepository
    {
        void AddBooking(Booking booking);
        IEnumerable<Booking> GetBookings(Guid hotelId, RoomType roomType);
    }
}