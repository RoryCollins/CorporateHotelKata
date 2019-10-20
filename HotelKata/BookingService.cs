using System;
using System.Linq;

namespace HotelKata
{
    public class BookingService
    {

        private readonly HotelService hotelService;

        public BookingService(HotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public Booking Book(Guid employeeId, Guid hotelId, RoomType roomType, string checkIn, string checkOut)
        {
            var hotel = hotelService.FindHotelBy(hotelId);
            var rooms = hotel.GetRoomsBy(roomType);
            if (rooms.Any())
            {
                return new Booking(employeeId, hotelId, roomType, checkIn, checkOut);
            }
            throw new RoomUnavailable();
        }
    }
}