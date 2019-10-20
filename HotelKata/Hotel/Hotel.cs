using System;
using System.Collections.Generic;
using System.Linq;
using HotelKata.Room;

namespace HotelKata.Hotel
{
    public class Hotel
    {

        internal readonly Guid hotelId;
        private readonly string hotelName;
        private readonly Dictionary<int, RoomType> rooms;

        public Hotel(Guid hotelId, string hotelName)
        {
            this.hotelId = hotelId;
            this.hotelName = hotelName;
            rooms = new Dictionary<int, RoomType>();
        }
        public virtual void SetRoom(int number, RoomType roomType)
        {
            rooms.Add(number, roomType);
        }

        public virtual IEnumerable<int> GetRoomsBy(RoomType roomType)
        {
            return rooms.Where(kvp => kvp.Value == roomType).Select(kvp => kvp.Key);
        }

        private bool Equals(Hotel other)
        {
            return hotelId.Equals(other.hotelId) && hotelName == other.hotelName && rooms.OrderBy(dict => dict.Key)
                       .SequenceEqual(other.rooms.OrderBy(dict => dict.Key));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Hotel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = hotelId.GetHashCode();
                hashCode = (hashCode * 397) ^ (hotelName != null ? hotelName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (rooms != null ? rooms.GetHashCode() : 0);
                return hashCode;
            }
        }

    }

    public class NoHotel : Hotel
    {
        public NoHotel() : base(Guid.Empty, "No Hotel")
        {
        }

        public override void SetRoom(int number, RoomType roomType) {}

        public override IEnumerable<int> GetRoomsBy(RoomType roomType)
        {
            return new List<int>();
        }
    }
}