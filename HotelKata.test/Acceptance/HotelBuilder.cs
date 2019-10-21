using System;
using System.Collections.Generic;
using HotelKata.Hotel;
using HotelKata.Room;

namespace HotelKata.test.Acceptance
{
    public class HotelBuilder
    {
        private Dictionary<int, RoomType> rooms = new Dictionary<int, RoomType>();
        private Guid hotelId;
        private const string hotelName = "The Overlook";

        public static HotelBuilder RegisterAHotel()
        {
            return new HotelBuilder();
        }

        public HotelBuilder WithId(Guid hotelId)
        {
            this.hotelId = hotelId;
            return this;
        }

        public HotelBuilder WithAStandardRoomAt(int number)
        {
            rooms.Add(number, RoomType.Standard);
            return this;
        }
        public HotelBuilder WithAMasterRoomAt(int number)
        {
            rooms.Add(number, RoomType.Master);
            return this;
        }

        public void To(HotelService hotelService)
        {
            hotelService.AddHotel(hotelId, hotelName);
            foreach (var room in rooms)
            {
                hotelService.SetRoom(hotelId, room.Key, room.Value);
            }
        }
    }
}