using AutoMapper;
using HRM.Business.Interface;
using HRM.Business.Models;
using HRM.Data.Interface;
using HRM.Data.Models;
using System;
using System.Collections.Generic;

namespace HRM.Business.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private IMapper mapper;
        public EmployeeManager(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Maps the EmployeeBusinessModel to Employee, fills the necessary attributes and calls repository
        /// </summary>
        /// <param name="employeeViewModel">Employee to be created</param>
        /// <returns>Create operation status (string)</returns>
        public string CreateEmployee(EmployeeBusinessModel employeeViewModel, string loggedInUserId)
        {
            var employee = mapper.Map<Employee>(employeeViewModel);
            if (employee.IsManager)
            {
                employee.ManagerId = null;
            }
            employee.CreatedDate = DateTime.UtcNow;
            employee.CreatedBy = loggedInUserId;
            employee.IsActive = true;
            return _employeeRepository.CreateEmployee(employee);
        }

        /// <summary>
        /// Maps the EmployeeBusinessModel to Employee, fills the necessary attributes and calls repository
        /// </summary>
        /// <param name="id">ID of Employee to be updated</param>
        /// <param name="employeeViewModel">Employee to be updated</param>
        /// <returns>Update operation status (string)</returns>
        public string UpdateEmployee(int id, EmployeeBusinessModel employeeViewModel, string loggedInUserId)
        {
            var employeeToDb = mapper.Map<Employee>(employeeViewModel);
            if (employeeToDb.IsManager)
            {
                employeeToDb.ManagerId = null;
            }
            employeeToDb.UpdatedDate = DateTime.UtcNow;
            employeeToDb.CreatedBy = loggedInUserId;
            return _employeeRepository.UpdateEmployee(employeeToDb);
        }

        /// <summary>
        /// Calls repository to delete the specified Employee
        /// </summary>
        /// <param name="id">ID of Employee to be deleted</param>
        /// <returns>Delete operation status (string)</returns>
        public string DeleteEmployee(int id)
        {
            return _employeeRepository.DeleteEmployee(id);
        }

        /// <summary>
        /// Gets the specified employee from database and maps into Business Model
        /// </summary>
        /// <param name="id">ID of Employee to fetch</param>
        /// <returns>Employee</returns>
        public EmployeeBusinessModel GetEmployee(int id)
        {
            var employeeFromDb = _employeeRepository.GetEmployee(id);
            var employee = mapper.Map<EmployeeBusinessModel>(employeeFromDb);
            return employee;
        }

        /// <summary>
        /// Gets all the employees from database and maps into Business Model
        /// </summary>
        /// <returns>List of employees from database</returns>
        public List<EmployeeBusinessModel> GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            var employeesBusinessModel = mapper.Map<List<EmployeeBusinessModel>>(employees);
            return employeesBusinessModel;
        }

        /// <summary>
        /// Gets all managers of the specified department from Database
        /// </summary>
        /// <param name="deptId">Department ID of the managers</param>
        /// <returns>Dictionary of Managers</returns>
        public Dictionary<int, string> GetManagers(int deptId)
        {
            var managersFromDb = _employeeRepository.GetManagers(deptId);
            var managers = new Dictionary<int, string>();
            foreach (var manager in managersFromDb)
            {
                managers.Add(manager.Id, manager.Name);
            }
            return managers;
        }
    }
}
