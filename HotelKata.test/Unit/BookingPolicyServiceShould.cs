using System;
using System.Collections.Generic;
using Xunit;
using static HotelKata.RoomType;

namespace HotelKata.test.Unit
{
    public class BookingPolicyServiceShould
    {
        [Fact]
        public void AllowEmployeeToBookTypeOfRoom()
        {
            var employeeId = Guid.NewGuid();
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            var bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository);
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType> {Standard}); 
            
            Assert.True(bookingPolicyService.isBookingAllowed(employeeId, Standard));
        }

        [Fact]
        public void PreventEmployeeFromBookingTypeOfRoom()
        {
            var employeeId = Guid.NewGuid();
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            var bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository);
            bookingPolicyService.SetEmployeePolicy(employeeId, new List<RoomType> {Standard});
            
            Assert.False(bookingPolicyService.isBookingAllowed(employeeId, Master));
        }

        [Fact]
        public void AllowAllRoomTypesIfNoPolicySet()
        {
            var employeeId = Guid.NewGuid();
            var bookingPolicyRepository = new InMemoryBookingPolicyRepository();
            var bookingPolicyService = new ProductionBookingPolicyService(bookingPolicyRepository);
            
            Assert.True(bookingPolicyService.isBookingAllowed(employeeId, Master));

        }
    }
}