using AutoMapper;
using HRM.Business.Interface;
using HRM.Business.Models;
using HRM.Data.Interface;
using HRM.Data.Models;
using System;
using System.Collections.Generic;

namespace HRM.Business.Manager
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository _departmentRepository;
        private IMapper mapper;
        public DepartmentManager(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Fetches all the departments from repository and maps with Business Model
        /// </summary>
        /// <returns></returns>
        public List<DepartmentBusinessModel> GetDepartments()
        {
            var departmentsFromDb = _departmentRepository.GetDepartments();
            var departments = mapper.Map<List<DepartmentBusinessModel>>(departmentsFromDb);
            return departments;
        }

        /// <summary>
        /// Maps the DepartmentBusinessModel to Department, fills the necessary attributes and calls repository
        /// </summary>
        /// <param name="departmentViewModel">Department to be created</param>
        /// <returns>Create operation status (string)</returns>
        public string CreateDepartment(DepartmentBusinessModel departmentViewModel)
        {
            var departmentToDb = mapper.Map<Department>(departmentViewModel);
            return _departmentRepository.CreateDepartment(departmentToDb);
        }

        /// <summary>
        /// Gets the specified department from database and maps into Business Model
        /// </summary>
        /// <param name="id">ID of Department to fetch</param>
        /// <returns>Department</returns>
        public DepartmentBusinessModel GetDepartment(int id)
        {
            var departmentFromDb = _departmentRepository.GetDepartment(id);
            var department = mapper.Map<DepartmentBusinessModel>(departmentFromDb);
            return department;
        }

        /// <summary>
        /// Maps the DepartmentBusinessModel to Department, fills the necessary attributes and calls repository
        /// </summary>
        /// <param name="id">ID of Employee to be updated</param>
        /// <param name="departmentViewModel">Department to be updated</param>
        /// <returns>Update operation status (string)</returns>
        public string UpdateDepartment(int id, DepartmentBusinessModel departmentViewModel, string loggedInUserId)
        {
            var departmentToDb = mapper.Map<Department>(departmentViewModel);
            return _departmentRepository.UpdateDepartment(departmentToDb);
        }

        /// <summary>
        /// Calls repository to delete the specified Department
        /// </summary>
        /// <param name="id">ID of Department to be deleted</param>
        /// <returns>Delete operation status (string)</returns>
        public string DeleteDepartment(int id)
        {
            return _departmentRepository.DeleteDepartment(id);
        }
    }
}
