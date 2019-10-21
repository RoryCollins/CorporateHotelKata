using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelKata.Company
{
    public interface EmployeeRepository
    {
        void Add(Guid companyId, Guid employeeId);
    }

    public class InMemoryEmployeeRepository : EmployeeRepository
    {
        private Dictionary<Guid, Guid> employees = new Dictionary<Guid, Guid>();
        public void Add(Guid companyId, Guid employeeId)
        {
            employees.Add(employeeId, companyId);
        }

        public Guid GetCompanyFor(Guid employeeId)
        {
            return employees.FirstOrDefault(it => it.Key == employeeId).Value;
        }
    }
}