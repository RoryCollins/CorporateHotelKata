using System;
using System.Linq;

namespace HotelKata
{
    public class BookingService
    {

        private readonly HotelService hotelService;
        private readonly BookingRepository bookingRepository;
        private readonly BookingPolicyService bookingPolicyService;

        public BookingService(HotelService hotelService, BookingRepository bookingRepository, BookingPolicyService bookingPolicyService)
        {
            this.hotelService = hotelService;
            this.bookingRepository = bookingRepository;
            this.bookingPolicyService = bookingPolicyService;
        }

        public Booking Book(Guid employeeId, Guid hotelId, RoomType roomType, string checkIn, string checkOut)
        {
            var hotel = hotelService.FindHotelBy(hotelId);
            var rooms = hotel.GetRoomsBy(roomType);
            if (!rooms.Any()) throw new RoomUnavailable();
            if(!bookingPolicyService.isBookingAllowed(employeeId, roomType)) throw new InsufficientPrivilege();
            if(rooms.Count() <= bookingRepository.GetBookings(hotelId, roomType).Count()) throw new RoomUnavailable();
            var booking = new Booking(employeeId, hotelId, roomType, checkIn, checkOut);
            bookingRepository.AddBooking(booking);
            return booking;
        }
        
        
    }
}