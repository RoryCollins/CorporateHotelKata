using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelKata
{
    public class Policy
    {
        public Guid EmployeeId { get; }
        private IEnumerable<RoomType> roomTypes;

        public Policy(Guid employeeId, IEnumerable<RoomType> roomTypes)
        {
            EmployeeId = employeeId;
            this.roomTypes = roomTypes;
        }

        public virtual bool IsValid(RoomType roomType)
        {
            return roomTypes.Contains(roomType);
        }
    }
}