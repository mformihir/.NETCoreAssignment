using HRM.Data.Context;
using HRM.Data.Interface;
using HRM.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public string CreateEmployee(Employee employee)
        {
            try
            {
                _context.Add(employee);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception)
            {
                return "Error Occurred";
            }
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Include(e => e.Department).Include(e => e.Manager).Where(e => e.Id == id).FirstOrDefault();
        }

        public List<Employee> GetEmployees()
        {
            var employees = _context.Employees.Include(e => e.Department).Include(e => e.Manager);
            return employees.ToList();
        }
    }
}
