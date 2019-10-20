using System;

namespace HotelKata
{
    public interface HotelRepository
    {
        void AddHotel(Guid hotelId, string hotelName);
        Hotel FindHotelBy(Guid hotelId);
    }
}