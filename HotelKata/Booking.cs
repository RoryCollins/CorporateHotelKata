using System;

namespace HotelKata
{
    public class Booking
    {
        protected bool Equals(Booking other)
        {
            return bookingId.Equals(other.bookingId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Booking) obj);
        }

        public override int GetHashCode()
        {
            return bookingId.GetHashCode();
        }

        private readonly Guid bookingId;
        private readonly Guid employeeId;
        private readonly Guid hotelId;
        private readonly RoomType roomType;
        private readonly string checkIn;
        private readonly string checkOut;

        public Booking(Guid employeeId, Guid hotelId, RoomType roomType, string checkIn, string checkOut)
        {
            bookingId = new Guid();
            this.employeeId = employeeId;
            this.hotelId = hotelId;
            this.roomType = roomType;
            this.checkIn = checkIn;
            this.checkOut = checkOut;
        }
    }
}