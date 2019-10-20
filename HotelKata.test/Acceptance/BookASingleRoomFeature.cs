using System;
using Xunit;
using static HotelKata.RoomType;

namespace HotelKata.test.Acceptance
{
    public class BookASingleRoomFeature
    {
        private readonly ProductionHotelService hotelService;
        private BookingRepository bookingRepository;

        public BookASingleRoomFeature()
        {
            HotelRepository hotelRepository = new InMemoryHotelRepository();
            hotelService = new ProductionHotelService(hotelRepository);
            bookingRepository = new InMemoryBookingRepository();
        }

        [Fact]
        public void TheOneWhereISuccessfullyBookASingleRoom()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 101, Standard);
            hotelService.SetRoom(hotelId, 102, Standard);
            hotelService.SetRoom(hotelId, 103, Standard);
            
            var bookingService = new BookingService(hotelService, bookingRepository);

            var employeeId = Guid.NewGuid();
            var checkIn = "12/10/2019";
            var checkOut = "19/10/2019";
            var expectedBooking = new Booking(employeeId, hotelId, Standard, checkIn, checkOut);
            var actualBooking = bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);
            
            Assert.Equal(expectedBooking, actualBooking);
        }

        [Fact]
        public void TheOneWhereThereAreNoRoomsAvailable()
        {
            var hotelId = Guid.NewGuid();
            hotelService.AddHotel(hotelId, "The Overlook");
            hotelService.SetRoom(hotelId, 1, Standard);
            
            var bookingService = new BookingService(hotelService, bookingRepository);
            var employeeId = Guid.NewGuid();
            var checkIn = "12/10/2019";
            var checkOut = "19/10/2019";
            bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut);

            Assert.Throws<RoomUnavailable>(() =>bookingService.Book(employeeId, hotelId, Standard, checkIn, checkOut));
        }

    }
}