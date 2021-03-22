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

        public string UpdateEmployee(int id, EmployeeBusinessModel employeeViewModel)
        {
            var employeeToDb = new Employee();
            employeeToDb.Id = id;
            employeeToDb.Name = employeeViewModel.Name;
            employeeToDb.Phone = employeeViewModel.Phone;
            employeeToDb.Salary = employeeViewModel.Salary;
            employeeToDb.Email = employeeViewModel.Email;
            employeeToDb.IsManager = employeeViewModel.IsManager;
            if (!employeeViewModel.IsManager) //If employee is not a Manager, it must have a ManagerId
            {
                employeeToDb.ManagerId = employeeViewModel.ManagerId;
            }
            employeeToDb.DepartmentId = employeeViewModel.DepartmentId;
            employeeToDb.UpdatedDate = DateTime.UtcNow;
            employeeToDb.UpdatedBy = 1; //TODO: Get loggedInUserId for employee.CreatedBy
            return _employeeRepository.UpdateEmployee(employeeToDb);
        }

        public string DeleteEmployee(int id)
        {
            return _employeeRepository.DeleteEmployee(id);
        }

        public EmployeeBusinessModel GetEmployee(int id)
        {
            var employeeFromDb = _employeeRepository.GetEmployee(id);
            if (employeeFromDb != null)
            {
                var employee = new EmployeeBusinessModel();
                employee.Id = employeeFromDb.Id;
                employee.Name = employeeFromDb.Name;
                employee.Department = employeeFromDb.Department.Name;
                employee.DepartmentId = employeeFromDb.Department.Id;
                employee.Email = employeeFromDb.Email;
                employee.IsManager = employeeFromDb.IsManager;
                if (employeeFromDb.ManagerId != null)
                {
                    employee.ManagerId = (int)employeeFromDb.ManagerId;
                    employee.Manager = employeeFromDb.Manager.Name;
                }
                employee.Phone = employeeFromDb.Phone;
                employee.Salary = employeeFromDb.Salary;
                return employee;
            }
            return null;
        }

        public List<EmployeeBusinessModel> GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
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
