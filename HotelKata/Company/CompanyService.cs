using System;

namespace HotelKata.Company
{
    public class CompanyService
    {
        private readonly EmployeeRepository employeeRepository;

        public CompanyService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void AddEmployee(Guid companyId, Guid employeeId)
        {
            employeeRepository.Add(companyId, employeeId);
        }
    }
}