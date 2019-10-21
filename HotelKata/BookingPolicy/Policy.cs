using System;
using System.Collections.Generic;
using System.Linq;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public class Policy
    {
        public Guid Id { get; }
        private readonly IEnumerable<RoomType> roomTypes;

        public Policy(Guid id, IEnumerable<RoomType> roomTypes)
        {
            this.Id = id;
            this.roomTypes = roomTypes;
        }

        public virtual bool IsValid(RoomType roomType)
        {
            return roomTypes.Contains(roomType);
        }
    }
}