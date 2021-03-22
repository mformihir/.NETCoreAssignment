using HRM.Data.Context;
using HRM.Data.Interface;
using HRM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EmployeeContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string CreateEmployee(Employee employee)
        {
            try
            {
                _logger.LogInformation("This is repository");
                _context.Add(employee);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return "Error Occurred";
            }
        }

        public string UpdateEmployee(Employee employee)
        {
            var employeeFromDb = _context.Employees.Find(employee.Id);
            if (employeeFromDb == null)
            {
                return "Not found";
            }
            try
            {
                employeeFromDb.Name = employee.Name;
                employeeFromDb.Phone = employee.Phone;
                employeeFromDb.Salary = employee.Salary;
                employeeFromDb.Email = employee.Email;
                employeeFromDb.IsManager = employee.IsManager;
                if (!employee.IsManager) //If employee is not a Manager, it must have a ManagerId
                {
                    employeeFromDb.ManagerId = employee.ManagerId;
                }
                employeeFromDb.DepartmentId = employee.DepartmentId;
                employeeFromDb.UpdatedDate = DateTime.UtcNow;
                employeeFromDb.UpdatedBy = 1; //TODO: Get loggedInUserId for employee.CreatedBy

                _context.Entry(employeeFromDb).State = EntityState.Modified;
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return "Error occurred";
            }
        }

        public string DeleteEmployee(int id)
        {
            var employeeFromDb = _context.Employees.Find(id);
            if (employeeFromDb == null)
            {
                return "Not found";
            }
            try
            {
                _context.Remove(employeeFromDb);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return "Error occured";
            }
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                return _context.Employees.Include(e => e.Department).Include(e => e.Manager).Where(e => e.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return null;
            }
        }
        public List<Employee> GetEmployees()
        {
            try
            {
                var employees = _context.Employees.Include(e => e.Department).Include(e => e.Manager);
                return employees.ToList();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return null;
            }
        }

        public List<Employee> GetManagers(int deptId)
        {
            try
            {
                var managers = _context.Employees.Where(e => e.DepartmentId == deptId && e.IsManager == true).ToList();
                return managers;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return null;
            }
        }
    }
}
