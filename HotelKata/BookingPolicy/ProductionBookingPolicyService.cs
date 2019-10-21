using System;
using System.Collections.Generic;
using HotelKata.Company;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public class ProductionBookingPolicyService : BookingPolicyService
    {
        private readonly BookingPolicyRepository bookingPolicyRepository;
        private readonly CompanyService companyService;

        public ProductionBookingPolicyService(BookingPolicyRepository bookingPolicyRepository, CompanyService companyService)
        {
            this.bookingPolicyRepository = bookingPolicyRepository;
            this.companyService = companyService;
        }

        public bool isBookingAllowed(Guid employeeId, RoomType roomType)
        {
            var employeePolicy = bookingPolicyRepository.PolicyFor(employeeId);
            var companyPolicy = bookingPolicyRepository.PolicyFor(companyService.FindCompanyByEmployee(employeeId));
            return employeePolicy.Or(companyPolicy).IsValid(roomType);
        }

        public void SetCompanyPolicy(Guid companyId, IEnumerable<RoomType> roomTypes)
        {
            bookingPolicyRepository.AddPolicy(companyId, roomTypes);
        }

        public void SetEmployeePolicy(Guid employeeId, IEnumerable<RoomType> roomTypes)
        {
            bookingPolicyRepository.AddPolicy(employeeId, roomTypes);
        }


    }

    static class Extension{
        public static Policy Or(this Policy policy, Policy other)
        {
            return policy.GetType()==typeof(NoPolicySet) ? other : policy;
        }
    }
    
}