using System;
using System.Collections.Generic;
using System.Linq;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public class InMemoryBookingPolicyRepository : BookingPolicyRepository
    {
        private readonly List<Policy> policies = new List<Policy>();
        public void AddPolicy(Guid employeeId, IEnumerable<RoomType> roomTypes)
        
        {
            policies.Add(new Policy(employeeId, roomTypes));
        }

        public Policy PolicyFor(Guid employeeId)
        {
            return policies.FirstOrDefault(it => it.EmployeeId == employeeId) ?? new NoPolicySet();
        }
    }
}