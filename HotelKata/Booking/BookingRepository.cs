using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.Booking
{
    public interface BookingRepository
    {
        void AddBooking(Booking booking);
        IEnumerable<Booking> GetBookings(Guid hotelId, RoomType roomType);
    }
}