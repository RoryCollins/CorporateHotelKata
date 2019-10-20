using System;
using HotelKata.Room;

namespace HotelKata.Hotel
{

    public class ProductionHotelService : HotelService
    {
        private readonly HotelRepository hotelRepository;

        public ProductionHotelService(HotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }
        public void AddHotel(Guid hotelId, string name)
        {
            hotelRepository.AddHotel(hotelId, name);
        }

        public void SetRoom(Guid hotelId, int number, RoomType roomType)
        {
            var hotel = hotelRepository.FindHotelBy(hotelId);
            hotel.SetRoom(number, roomType);
        }


        public Hotel FindHotelBy(Guid hotelId)
        {
            var hotel = hotelRepository.FindHotelBy(hotelId);
            if(hotel.Equals(new NoHotel()))
                throw new NoHotelFound();
            return hotel;
        }
    }
}