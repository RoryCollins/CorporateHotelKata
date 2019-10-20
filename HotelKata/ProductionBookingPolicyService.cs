using System;

namespace HotelKata
{
    public class ProductionBookingPolicyService : BookingPolicyService
    {
        public bool isBookingAllowed(Guid employeeId, RoomType master)
        {
            throw new NotImplementedException();
        }
    }
}