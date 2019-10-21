using System;
using HotelKata.BookingPolicy;
using HotelKata.Company;
using Moq;
using Xunit;

namespace HotelKata.test.Unit
{
    public class CompanyServiceShould
    {
        [Fact]
        public void AddEmployeeToRepository()
        {
            var employeeRepository = new Mock<EmployeeRepository>();
            var companyService = new ProductionCompanyService(employeeRepository.Object);
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            companyService.AddEmployee(companyId, employeeId);
            employeeRepository.Verify(it=>it.Add(companyId, employeeId));
        }

        [Fact]
        public void ReturnTheCompanyIdForAGivenEmployee()
        {
            
            var employeeRepository = new Mock<EmployeeRepository>();
            var companyService = new ProductionCompanyService(employeeRepository.Object);
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            employeeRepository.Setup(it => it.GetCompanyFor(employeeId)).Returns(companyId);

            Assert.Equal(companyId, companyService.FindCompanyByEmployee(employeeId));
        }
        
    }
}