using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public interface BookingPolicyRepository
    {
        void AddPolicy(Guid id, IEnumerable<RoomType> roomTypes);
        Policy PolicyFor(Guid employeeId, Guid companyId);
    }
}