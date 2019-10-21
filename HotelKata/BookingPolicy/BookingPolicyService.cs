using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public interface BookingPolicyService
    {

        void SetCompanyPolicy(Guid companyId, IEnumerable<RoomType> roomTypes);
        void SetEmployeePolicy(Guid employeeId, IEnumerable<RoomType> roomTypes);
        bool isBookingAllowed(Guid employeeId, RoomType master);
    }
}