using HRM.Business.Models;
using System.Collections.Generic;

namespace HRM.Business.Interface
{
    public interface IDepartmentManager
    {
        List<DepartmentBusinessModel> GetDepartments();

        string CreateDepartment(DepartmentBusinessModel departmentViewModel);

        DepartmentBusinessModel GetDepartment(int id);

        string UpdateDepartment(int id, DepartmentBusinessModel departmentViewModel, string loggedInUserId);

        string DeleteDepartment(int id);
    }
}
