using HRM.Data.Context;
using HRM.Data.Interface;
using HRM.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeContext _context;
        private readonly ILogger<DepartmentRepository> _logger;
        public DepartmentRepository(EmployeeContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Fetches the list of all the Departments in the database
        /// </summary>
        /// <returns>List of Departments</returns>
        public List<Department> GetDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Adds the Department to the database
        /// </summary>
        /// <param name="department">Department to be created</param>
        /// <returns>Create Operation Status/Message (string)</returns>
        public string CreateDepartment(Department department)
        {
            try
            {
                _context.Add(department);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return "Error Occurred";
            }
        }
    }
}
