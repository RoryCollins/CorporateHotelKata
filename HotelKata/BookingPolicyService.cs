using System;
using System.Collections.Generic;

namespace HotelKata
{
    public interface BookingPolicyService
    {
        bool isBookingAllowed(Guid employeeId, RoomType master);

        void SetEmployeePolicy(Guid employeeId, IEnumerable<RoomType> roomTypes);
    }
}