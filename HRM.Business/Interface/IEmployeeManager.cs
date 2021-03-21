using HRM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Business.Interface
{
    public interface IEmployeeManager
    {
        EmployeeBusinessModel Get(int id);
        List<EmployeeBusinessModel> GetEmployees();
        public string CreateEmployee(EmployeeBusinessModel employee);
    }
}
