using HRM.Business.Interface;
using HRM.Business.Models;
using HRM.Data.Interface;
using HRM.Data.Models;
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

        /// <summary>
        /// Maps the DepartmentBusinessModel to Department, fills the necessary attributes and calls repository
        /// </summary>
        /// <param name="departmentViewModel">Department to be created</param>
        /// <returns>Create operation status (string)</returns>
        public string CreateDepartment(DepartmentBusinessModel departmentViewModel)
        {
            var departmentToDb = new Department();
            departmentToDb.Name = departmentViewModel.Name;
            return _departmentRepository.CreateDepartment(departmentToDb);
        }
    }
}
