using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    class NoPolicySet : Policy
    {
        public NoPolicySet() : base(Guid.Empty, new List<RoomType>()) { }

        public override bool IsValid(RoomType roomType)
        {
            return true;
        }
    }
}