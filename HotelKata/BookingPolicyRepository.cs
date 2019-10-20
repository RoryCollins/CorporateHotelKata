using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelKata
{
    public interface BookingPolicyRepository
    {
        void AddPolicy(Guid employeeId, IEnumerable<RoomType> roomTypes);
        Policy PolicyFor(Guid employeeId);
    }

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