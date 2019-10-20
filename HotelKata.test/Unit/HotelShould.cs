using System;
using Xunit;
using static HotelKata.Room.RoomType;

namespace HotelKata.test.Unit
{
    public class HotelShould
    {
        private readonly Hotel.Hotel hotel;

        public HotelShould()
        {
            hotel = new Hotel.Hotel(Guid.NewGuid(), "Hotel California");
            hotel.SetRoom(101, Standard);
        }

        [Fact]
        public void StoreAndRetrieveRooms()
        {
            Assert.Contains(101, hotel.GetRoomsBy(Standard));
        }
        [Fact]
        public void ReturnEmptyListWhenNoRoomsFound()
        {
            Assert.Empty(hotel.GetRoomsBy(Master));
        }
    }
}