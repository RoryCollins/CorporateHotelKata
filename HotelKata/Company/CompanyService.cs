using System;

namespace HotelKata.Company
{
    public interface CompanyService
    {
        void AddEmployee(Guid companyId, Guid employeeId);
        Guid FindCompanyByEmployee(Guid employeeId);
    }
}