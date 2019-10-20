using System;
using System.Collections.Generic;

namespace HotelKata
{
    public class InMemoryHotelRepository : HotelRepository
    {
        private List<Hotel> hotels;

        public InMemoryHotelRepository()
        {
            hotels = new List<Hotel>();
        }
        public void AddHotel(Guid hotelId, string hotelName)
        {
            hotels.Add(new Hotel(hotelId, hotelName));
        }

        public Hotel FindHotelBy(Guid hotelId)
        {
            return hotels.Find(it => it.hotelId == hotelId) ?? new NoHotel();
        }
    }
}