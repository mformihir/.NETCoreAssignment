using HRM.Data.Context;
using HRM.Data.Interface;
using HRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeContext _context;
        public DepartmentRepository(EmployeeContext context)
        {
            _context = context;
        }
        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
