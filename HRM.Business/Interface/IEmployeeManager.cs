using HRM.Business.Models;
using System.Collections.Generic;

namespace HRM.Business.Interface
{
    public interface IEmployeeManager
    {
        EmployeeBusinessModel GetEmployee(int id);
        List<EmployeeBusinessModel> GetEmployees();
        string CreateEmployee(EmployeeBusinessModel employee, string loggedInUserId);
        Dictionary<int, string> GetManagers(int deptId);
        string UpdateEmployee(int id, EmployeeBusinessModel employee, string loggedInUserId);
        string DeleteEmployee(int id);
    }
}
