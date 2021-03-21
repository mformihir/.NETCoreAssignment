using HRM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Business.Interface
{
    public interface IDepartmentManager
    {
        List<DepartmentBusinessModel> GetDepartments();
    }
}
