using System;
using HotelKata.Company;
using Xunit;

namespace HotelKata.test.Unit
{
    public class InMemoryEmployeeRepositoryShould
    {
        [Fact]
        public void StoreAndRetrieveEmployeesByCompany()
        {
            var employeeRepository = new InMemoryEmployeeRepository();
            var hoteliersInc = Guid.NewGuid();
            var staff2PleaseU = Guid.NewGuid();
            var bob = Guid.NewGuid();
            var geoff = Guid.NewGuid();
            var lisa = Guid.NewGuid();
            employeeRepository.Add(hoteliersInc, bob);
            employeeRepository.Add(hoteliersInc, geoff);
            employeeRepository.Add(staff2PleaseU, lisa);
            
            Assert.Equal(staff2PleaseU,employeeRepository.GetCompanyFor(lisa));
        }
    }
}