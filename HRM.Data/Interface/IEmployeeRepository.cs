using HRM.Data.Models;
using System.Collections.Generic;

namespace HRM.Data.Interface
{
    public interface IEmployeeRepository
    {
        Employee Get(int id);
        List<Employee> Get();
        string CreateEmployee(Employee employee);
    }
}
