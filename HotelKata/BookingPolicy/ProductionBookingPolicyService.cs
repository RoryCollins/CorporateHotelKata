using System;
using System.Collections.Generic;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public class ProductionBookingPolicyService : BookingPolicyService
    {
        private BookingPolicyRepository bookingPolicyRepository;

        public ProductionBookingPolicyService(BookingPolicyRepository bookingPolicyRepository)
        {
            this.bookingPolicyRepository = bookingPolicyRepository;
        }

        public bool isBookingAllowed(Guid employeeId, RoomType roomType)
        {
            return bookingPolicyRepository.PolicyFor(employeeId).IsValid(roomType);

        }

        public void SetCompanyPolicy(Guid companyId, IEnumerable<RoomType> roomTypes)
        {
            throw new NotImplementedException();
        }

        public void SetEmployeePolicy(Guid employeeId, IEnumerable<RoomType> roomTypes)
        {
            bookingPolicyRepository.AddPolicy(employeeId, roomTypes);
        }
    }
}