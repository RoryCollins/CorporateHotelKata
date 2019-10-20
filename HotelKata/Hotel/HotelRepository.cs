using System;

namespace HotelKata.Hotel
{
    public interface HotelRepository
    {
        void AddHotel(Guid hotelId, string hotelName);
        Hotel FindHotelBy(Guid hotelId);
    }
}