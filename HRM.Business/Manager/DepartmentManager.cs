using HRM.Business.Interface;
using HRM.Business.Models;
using HRM.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Business.Manager
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public List<DepartmentBusinessModel> GetDepartments()
        {
            var departmentsFromDb = _departmentRepository.GetDepartments();
            var departments = new List<DepartmentBusinessModel>();
            foreach (var item in departmentsFromDb)
            {
                var dept = new DepartmentBusinessModel();
                dept.Id = item.Id;
                dept.Name = item.Name;
                departments.Add(dept);
            }
            return departments;
        }
    }
}
