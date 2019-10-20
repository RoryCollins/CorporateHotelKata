using System;
using System.Collections.Generic;

namespace HotelKata
{
    public interface BookingPolicyRepository
    {
        void AddPolicy(Guid employeeId, IEnumerable<RoomType> roomTypes);
        Policy PolicyFor(Guid employeeId);
    }
}