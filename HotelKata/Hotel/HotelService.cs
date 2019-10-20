using System;
using HotelKata.Room;

namespace HotelKata.Hotel
{
    public interface HotelService
    {
        void AddHotel(Guid hotelId, string name);
        void SetRoom(Guid hotelId, int number, RoomType roomType);
        Hotel FindHotelBy(Guid hotelId);
    }
}