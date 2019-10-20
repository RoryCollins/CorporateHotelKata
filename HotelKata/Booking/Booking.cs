using System;
using HotelKata.Room;

namespace HotelKata.Booking
{
    public class Booking
    {
        protected bool Equals(Booking other)
        {
            return bookingId.Equals(other.bookingId) && EmployeeId.Equals(other.EmployeeId) && HotelId.Equals(other.HotelId) && RoomType == other.RoomType && CheckIn.Equals(other.CheckIn) && CheckOut.Equals(other.CheckOut);
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
            unchecked
            {
                var hashCode = bookingId.GetHashCode();
                hashCode = (hashCode * 397) ^ EmployeeId.GetHashCode();
                hashCode = (hashCode * 397) ^ HotelId.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) RoomType;
                hashCode = (hashCode * 397) ^ CheckIn.GetHashCode();
                hashCode = (hashCode * 397) ^ CheckOut.GetHashCode();
                return hashCode;
            }
        }

        private readonly Guid bookingId;
        public Guid EmployeeId { get; }
        public Guid HotelId { get; }
        public RoomType RoomType { get; }
        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }

        public Booking(Guid employeeId, Guid hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut, Guid bookingId)
        {
            this.EmployeeId = employeeId;
            this.HotelId = hotelId;
            this.RoomType = roomType;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.bookingId = bookingId;
        }
    }
}