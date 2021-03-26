using HRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Data.Interface
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();

        string CreateDepartment(Department department);
    }
}
