using System;
using System.Linq;
using HotelKata.BookingPolicy;
using HotelKata.Hotel;
using HotelKata.Room;

namespace HotelKata.Booking
{
    public class BookingService
    {

        private readonly HotelService hotelService;
        private readonly BookingRepository bookingRepository;
        private readonly BookingPolicyService bookingPolicyService;
        private readonly IdGenerator idGenerator;

        public BookingService(HotelService hotelService, BookingRepository bookingRepository, BookingPolicyService bookingPolicyService, IdGenerator idGenerator)
        {
            this.hotelService = hotelService;
            this.bookingRepository = bookingRepository;
            this.bookingPolicyService = bookingPolicyService;
            this.idGenerator = idGenerator;
        }

        public Booking Book(Guid employeeId, Guid hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            
            var booking = new Booking(employeeId, hotelId, roomType, checkIn, checkOut, idGenerator.GenerateId());
            ValidateBooking(booking);
            bookingRepository.AddBooking(booking);
            return booking;
        }

        public void ValidateBooking(Booking booking)
        {
            if(booking.CheckOut <= booking.CheckIn) throw new CheckoutDateInvalid();
            var hotel = hotelService.FindHotelBy(booking.HotelId);
            var rooms = hotel.GetRoomsBy(booking.RoomType).ToList();
            if (!rooms.Any()) throw new RoomUnavailable();
            if(rooms.Count() <= bookingRepository.GetBookings(booking.HotelId, booking.RoomType).Count()) throw new RoomUnavailable();
            if(!bookingPolicyService.isBookingAllowed(booking.EmployeeId, booking.RoomType)) throw new InsufficientPrivilege();
        }
        
    }
}