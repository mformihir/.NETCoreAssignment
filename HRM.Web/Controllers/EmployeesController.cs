using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data.Context;
using HRM.Data.Models;
using HRM.Business.Interface;
using HRM.Business.Models;

namespace HRM.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IDepartmentManager _departmentManager;

        public EmployeesController(IEmployeeManager employeeManager, IDepartmentManager departmentManager)
        {
            _employeeManager = employeeManager;
            _departmentManager = departmentManager;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var employees = _employeeManager.GetEmployees();
            return View(employees);
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeManager.GetEmployee((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_departmentManager.GetDepartments(), "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_employeeManager.GetEmployees(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,DepartmentId,Salary,IsManager,ManagerId,Phone,Email")] EmployeeBusinessModel employee)
        {
            if (ModelState.IsValid)
            {
                var result = _employeeManager.CreateEmployee(employee);
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Error", "Home");
            }
            ViewData["DepartmentId"] = new SelectList(_departmentManager.GetDepartments(), "Id", "Name", employee.DepartmentId);
            ViewData["ManagerId"] = new SelectList(_employeeManager.GetEmployees(), "Id", "Name", employee.ManagerId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
        //    ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Email", employee.ManagerId);
        //    return View(employee);
        //}

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId,Salary,IsManager,ManagerId,Phone,Email,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy,IsActive")] Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(employee);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EmployeeExists(employee.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
        //    ViewData["ManagerId"] = new SelectList(_context.Employees, "Id", "Email", employee.ManagerId);
        //    return View(employee);
        //}

        // GET: Employees/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .Include(e => e.Department)
        //        .Include(e => e.Manager)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        // POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
