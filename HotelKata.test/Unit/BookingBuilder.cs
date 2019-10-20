using System;
using static HotelKata.RoomType;

namespace HotelKata.test.Unit
{
    public class BookingBuilder
    {
        private Guid employeeId = Guid.NewGuid();
        private Guid hotelId = Guid.NewGuid();
        private string checkIn = "01/04/2018";
        private string checkOut = "03/01/2018";
        private RoomType roomType = Standard;

        public static BookingBuilder aBooking()
        {
            return new BookingBuilder();
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


        public BookingBuilder From(string checkIn)
        {
            this.checkIn = checkIn;
            return this;
        }

        public BookingBuilder To(string checkOut)
        {
            this.checkOut = checkOut;
            return this;
        }

        public BookingBuilder WithRoomType(RoomType roomType)
        {
            this.roomType = roomType;
            return this;
        }
        public Booking Build()
        {
            return new Booking(employeeId, hotelId, roomType, checkIn, checkOut);
        }
    }
}