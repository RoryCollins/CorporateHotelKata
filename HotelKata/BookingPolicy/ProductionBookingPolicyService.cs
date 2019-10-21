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
            var companyId = companyService.FindCompanyByEmployee(employeeId);
            return bookingPolicyRepository.PolicyFor(employeeId, companyId).IsValid(roomType);
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
}