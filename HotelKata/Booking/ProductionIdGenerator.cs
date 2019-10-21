using System;

namespace HotelKata.Booking
{
    public class ProductionIdGenerator : IdGenerator
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}