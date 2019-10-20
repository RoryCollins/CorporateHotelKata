using System;
using System.Runtime.CompilerServices;
using HotelKata.Room;
using static HotelKata.Room.RoomType;

namespace HotelKata.test.Unit
{
    public class BookingBuilder
    {
        private Guid employeeId = Guid.NewGuid();
        private Guid hotelId = Guid.NewGuid();
        private DateTime checkIn = DateTime.Parse("01/04/2018");
        private DateTime checkOut = DateTime.Parse("03/01/2018");
        private RoomType roomType = Standard;
        private Guid bookingId = Guid.NewGuid();

        public static BookingBuilder aBooking()
        {
            return new BookingBuilder();
        }

        public BookingBuilder WithId(Guid bookingId)
        {
            this.bookingId = bookingId;
            return this;
        }

        public BookingBuilder WithEmployeeId(Guid employeeId)
        {
            this.employeeId = employeeId;
            return this;
        }

        public BookingBuilder WithHotelId(Guid hotelId)
        {
            this.hotelId = hotelId;
            return this;
        }


        public BookingBuilder From(DateTime checkIn)
        {
            this.checkIn = checkIn;
            return this;
        }

        public BookingBuilder To(DateTime checkOut)
        {
            this.checkOut = checkOut;
            return this;
        }

        public BookingBuilder WithRoomType(RoomType roomType)
        {
            this.roomType = roomType;
            return this;
        }
        public Booking.Booking Build()
        {
            return new Booking.Booking(employeeId, hotelId, roomType, checkIn, checkOut, bookingId);
        }
    }
}