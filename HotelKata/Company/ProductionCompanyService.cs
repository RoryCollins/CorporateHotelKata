using System;

namespace HotelKata.Company
{
    public class ProductionCompanyService : CompanyService
    {
        private readonly EmployeeRepository employeeRepository;

        public ProductionCompanyService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void AddEmployee(Guid companyId, Guid employeeId)
        {
            employeeRepository.Add(companyId, employeeId);
        }

        public Guid FindCompanyByEmployee(Guid employeeId)
        {
            return employeeRepository.GetCompanyFor(employeeId);
        }
    }
}