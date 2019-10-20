using System;
using Moq;
using Xunit;

namespace HotelKata.test.Unit
{
    public class HotelServiceShould
    {
        private static readonly Mock<HotelRepository> HotelRepository;
        private static readonly ProductionHotelService HotelService;


        static HotelServiceShould()
        {
            HotelRepository = new Mock<HotelRepository>();
            HotelService = new ProductionHotelService(HotelRepository.Object);
           ;
        }

        [Fact]
        public void AddHotelToRepository()
        {
            var hotelId = Guid.NewGuid();
            var hotelName = "The Grand Budapest Hotel";
            HotelService.AddHotel(hotelId, hotelName);
            HotelRepository.Verify(hr => hr.AddHotel(hotelId, hotelName));
        }

        [Fact]
        public void SetStandardRoom()
        {
            var hotelId = Guid.NewGuid();
            var hotelName = "The Grand Budapest Hotel";
            var hotel = new Hotel(hotelId, hotelName);
            HotelRepository.Setup(it => it.FindHotelBy(hotelId)).Returns(hotel);
                
            HotelService.SetRoom(hotelId, 101, RoomType.Standard);
            
            
            Assert.Equal(hotel, HotelService.FindHotelBy(hotelId));
        }

        [Fact]
        public void ThrowExceptionIfHotelCannotBeFound()
        {
            
            var hotelId = Guid.NewGuid();
            HotelRepository.Setup(it => it.FindHotelBy(hotelId)).Returns(new NoHotel());

            Assert.Throws<NoHotelFound>(() => HotelService.FindHotelBy(hotelId));

        }
    }
}