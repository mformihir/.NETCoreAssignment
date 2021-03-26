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

        /// <summary>
        /// Adds the Employee to the database
        /// </summary>
        /// <param name="employee">Employee to be created</param>
        /// <returns>Create Operation Status/Message (string)</returns>
        public string CreateEmployee(Employee employee)
        {
            try
            {
                _context.Add(employee);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return "Error Occurred";
            }
        }

        /// <summary>
        /// Updates the existing Employee from the database
        /// </summary>
        /// <param name="employee">Updated employee object</param>
        /// <returns>Update Operation Status/Message (string)</returns>
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
                _logger.LogCritical(e.Message, e.StackTrace);
                return "Error occurred";
            }
        }

        /// <summary>
        /// Deletes the Employee identified by specified id
        /// </summary>
        /// <param name="id">id of Employee to delete</param>
        /// <returns>Delete Operation Status/Message (string)</returns>
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
                _logger.LogCritical(e.Message, e.StackTrace);
                return "Error occured";
            }
        }

        /// <summary>
        /// Returns the Employee of the specified id from database
        /// </summary>
        /// <param name="id">id of Employee to fetch</param>
        /// <returns>Employee</returns>
        public Employee GetEmployee(int id)
        {
            try
            {
                return _context.Employees.Include(e => e.Department).Include(e => e.Manager).Where(e => e.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Gets all the Employees from the database
        /// </summary>
        /// <returns>Employees</returns>
        public List<Employee> GetEmployees()
        {
            try
            {
                var employees = _context.Employees.Include(e => e.Department).Include(e => e.Manager);
                return employees.ToList();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Gets all the managers of the specified department from database
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns>Managers</returns>
        public List<Employee> GetManagers(int deptId)
        {
            try
            {
                var managers = _context.Employees.Where(e => e.DepartmentId == deptId && e.IsManager == true).ToList();
                return managers;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return null;
            }
        }
    }
}
