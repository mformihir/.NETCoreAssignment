using HRM.Data.Models;
using System.Collections.Generic;

namespace HRM.Data.Interface
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        List<Employee> GetEmployees();
        string CreateEmployee(Employee employee);
        List<Employee> GetManagers(int deptId);
        string UpdateEmployee(Employee employee);
        string DeleteEmployee(int id);
    }
}
