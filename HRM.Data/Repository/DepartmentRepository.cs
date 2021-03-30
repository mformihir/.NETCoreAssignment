using HRM.Data.Context;
using HRM.Data.Interface;
using HRM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Returns the Department of the specified id from database
        /// </summary>
        /// <param name="id">id of Department to fetch</param>
        /// <returns>Department</returns>
        public Department GetDepartment(int id)
        {
            try
            {
                return _context.Departments.Where(e => e.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Updates the existing Department from the database
        /// </summary>
        /// <param name="department">Updated department object</param>
        /// <returns>Update Operation Status/Message (string)</returns>
        public string UpdateDepartment(Department department)
        {
            var departmentFromDb = _context.Departments.Find(department.Id);
            if (departmentFromDb == null)
            {
                return "Not found";
            }
            try
            {
                departmentFromDb.Name = department.Name;
                _context.Entry(departmentFromDb).State = EntityState.Modified;
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
        /// Deletes the Department identified by specified id
        /// </summary>
        /// <param name="id">id of Department to delete</param>
        /// <returns>Delete Operation Status/Message (string)</returns>
        public string DeleteDepartment(int id)
        {
            var departmentFromDb = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (departmentFromDb == null)
            {
                return "Not found";
            }
            try
            {
                if (_context.Employees.Where(e => e.DepartmentId == departmentFromDb.Id).Any())
                {
                    return "Can't remove a department when there are one or more employees in the department";
                }
                _context.Remove(departmentFromDb);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e.StackTrace);
                return "Error occured";
            }
        }

    }
}
