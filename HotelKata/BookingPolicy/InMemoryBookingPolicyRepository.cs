using System;
using System.Collections.Generic;
using System.Linq;
using HotelKata.Room;

namespace HotelKata.BookingPolicy
{
    public class InMemoryBookingPolicyRepository : BookingPolicyRepository
    {
        private readonly List<Policy> policies = new List<Policy>();
        public void AddPolicy(Guid Id, IEnumerable<RoomType> roomTypes)
        {
            policies.Add(new Policy(Id, roomTypes));
        }

        public Policy PolicyFor(Guid employeeId, Guid companyId)
        {
            return policies.FirstOrDefault(it => it.Id == employeeId) ?? 
                   policies.FirstOrDefault(it => it.Id == companyId) ??
                   new NoPolicySet();
        }
    }
}