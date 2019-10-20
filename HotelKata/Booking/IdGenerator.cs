using System;
using HotelKata.Room;

namespace HotelKata.Booking
{
    public interface IdGenerator
    {
        Guid GenerateId();
        
    }
}