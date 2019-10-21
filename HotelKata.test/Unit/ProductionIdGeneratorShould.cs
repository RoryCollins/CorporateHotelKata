using HotelKata.Booking;
using Xunit;

namespace HotelKata.test.Unit
{
    public class ProductionIdGeneratorShould
    {
        [Fact]
        public void GenerateNonIdenticalIds()
        {
            var idGenerator = new ProductionIdGenerator();
            var firstId = idGenerator.GenerateId();
            var secondId = idGenerator.GenerateId();
            
            Assert.NotEqual(firstId, secondId);
        }
    }
}