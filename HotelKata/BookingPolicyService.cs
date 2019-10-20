using System;

namespace HotelKata
{
    public interface BookingPolicyService
    {
        bool isBookingAllowed(Guid employeeId, RoomType master);
    }
}