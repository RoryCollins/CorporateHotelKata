using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public interface BookingPolicyService
    {
        bool isBookingAllowed(Guid employeeId, RoomType master);

        void SetEmployeePolicy(Guid employeeId, IEnumerable<RoomType> roomTypes);
    }
}