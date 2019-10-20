using System;
using Xunit;

namespace HotelKata.test.Unit
{
    public class InMemoryHotelRepositoryShould
    {
        private static readonly InMemoryHotelRepository HotelRepository = new InMemoryHotelRepository();

        [Fact]
        public void StoreAndRetrieveHotels()
        {
            var hotelId = Guid.NewGuid();
            var hotelName = "Hotel Transylvania";
            HotelRepository.AddHotel(hotelId, hotelName);
            var hotel = new Hotel(hotelId, hotelName);
            Assert.Equal(hotel, HotelRepository.FindHotelBy(hotelId));
        }

        [Fact]
        public void ReturnNoHotelIfHotelNotFound()
        {
            var hotelId = Guid.NewGuid();
            Assert.Equal(new NoHotel(), HotelRepository.FindHotelBy(hotelId));
        }
    }
}