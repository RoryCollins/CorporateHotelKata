using System;
using System.Collections.Generic;
using HotelKata.BookingPolicy;
using HotelKata.Company;
using HotelKata.Room;
using Moq;
using Xunit;
using static HotelKata.Room.RoomType;

namespace HotelKata.test.Unit
{
    public class BookingPolicyServiceShould
    {
        private readonly Guid employeeId = Guid.NewGuid();
        private readonly BookingPolicyRepository bookingPolicyRepository = new InMemoryBookingPolicyRepository();
        private readonly BookingPolicyService bookingPolicyService;
        private readonly Mock<CompanyService> companyService = new Mock<CompanyService>();

        public BookingPolicyServiceShould()
        {
            
            bookingPolicyService= new ProductionBookingPolicyService(bookingPolicyRepository, companyService.Object);
        }

        [Fact]
        public void AllowEmployeeToBookTypeOfRoom()
        {
            
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType> {Standard}); 
            
            Assert.True(bookingPolicyService.isBookingAllowed(employeeId, Standard));
        }

        [Fact]
        public void PreventEmployeeFromBookingTypeOfRoom()
        {
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType> {Standard});
            
            Assert.False(bookingPolicyService.isBookingAllowed(employeeId, Master));
        }

        [Fact]
        public void AllowAllRoomTypesIfNoPolicySet()
        {
            Assert.True(bookingPolicyService.isBookingAllowed(employeeId, Master));
        }

        [Fact]
        public void AllowCompanyEmployeeToBookTypeOfRoom()
        {
            var companyId = Guid.NewGuid();
            
            bookingPolicyService.SetCompanyPolicy(companyId, new List<RoomType>{Standard});
            Assert.True(bookingPolicyService.isBookingAllowed(employeeId, Standard));
        }

        [Fact]
        public void PreventCompanyEmployeesFromBookingTypeOfRoom()
        {
            var companyId = Guid.NewGuid();
            companyService.Setup(it => it.FindCompanyByEmployee(employeeId)).Returns(companyId);
            
            bookingPolicyService.SetCompanyPolicy(companyId, new List<RoomType>{Standard});
            Assert.False(bookingPolicyService.isBookingAllowed(employeeId, Master));
        }
    }
}