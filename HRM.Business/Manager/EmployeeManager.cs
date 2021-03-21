using HRM.Business.Interface;
using HRM.Business.Models;
using HRM.Data.Interface;
using HRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Business.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string CreateEmployee(EmployeeBusinessModel employeeViewModel)
        {
            var employee = new Employee();
            employee.Name = employeeViewModel.Name;
            employee.Phone = employeeViewModel.Phone;
            employee.Salary = employeeViewModel.Salary;
            employee.Email = employeeViewModel.Email;
            employee.IsManager = employeeViewModel.IsManager;
            if (!employeeViewModel.IsManager) //If employee is not a Manager, it must have a ManagerId
            {
                employee.ManagerId = employeeViewModel.ManagerId;
            }
            employee.DepartmentId = employeeViewModel.DepartmentId;
            employee.CreatedDate = DateTime.UtcNow;
            //TODO: Get loggedInUserId for employee.CreatedBy
            employee.CreatedBy = 1;
            employee.IsActive = true;

            return _employeeRepository.CreateEmployee(employee);
        }

        public EmployeeBusinessModel Get(int id)
        {
            var employee = _employeeRepository.Get(id);
            return new EmployeeBusinessModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department.Name,
                Email = employee.Email,
                IsManager = employee.IsManager,
                Manager = employee.Manager.Name,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
        }

        public List<EmployeeBusinessModel> GetEmployees()
        {
            var employees = _employeeRepository.Get();
            var employeesBusinessModel = new List<EmployeeBusinessModel>();
            foreach (var employee in employees)
            {
                var employeeBusinessModel = new EmployeeBusinessModel();
                employeeBusinessModel.Id = employee.Id;
                employeeBusinessModel.Name = employee.Name;
                employeeBusinessModel.Email = employee.Email;
                employeeBusinessModel.Department = employee.Department.Name;
                employeeBusinessModel.IsManager = employee.IsManager;
                if (employee.Manager != null)
                {
                    employeeBusinessModel.Manager = employee.Manager.Name;
                }
                employeeBusinessModel.Phone = employee.Phone;
                employeeBusinessModel.Salary = employee.Salary;
                employeesBusinessModel.Add(employeeBusinessModel);
            }
            return employeesBusinessModel;
        }
    }
}
