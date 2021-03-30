using HRM.Data.Models;
using System.Collections.Generic;

namespace HRM.Data.Interface
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        string CreateDepartment(Department department);
        Department GetDepartment(int id);
        string UpdateDepartment(Department department);

        string DeleteDepartment(int id);
    }
}
