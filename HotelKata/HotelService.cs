using System;

namespace HotelKata
{
    public interface HotelService
    {
        void AddHotel(Guid hotelId, string name);
        void SetRoom(Guid hotelId, int number, RoomType roomType);
        Hotel FindHotelBy(Guid hotelId);
    }
}