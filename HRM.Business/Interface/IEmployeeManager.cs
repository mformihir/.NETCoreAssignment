using HRM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Business.Interface
{
    public interface IEmployeeManager
    {
        EmployeeBusinessModel GetEmployee(int id);
        List<EmployeeBusinessModel> GetEmployees();
        string CreateEmployee(EmployeeBusinessModel employee);
        Dictionary<int, string> GetManagers(int deptId);
        string UpdateEmployee(int id, EmployeeBusinessModel employee);
        string DeleteEmployee(int id);
    }
}
