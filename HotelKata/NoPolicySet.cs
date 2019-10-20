using System;
using System.Collections.Generic;

namespace HotelKata
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