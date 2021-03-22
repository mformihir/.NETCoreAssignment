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
using Microsoft.AspNetCore.Authorization;

namespace HRM.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IDepartmentManager _departmentManager;

        public EmployeesController(IEmployeeManager employeeManager, IDepartmentManager departmentManager)
        {
            _employeeManager = employeeManager;
            _departmentManager = departmentManager;
        }

        public IActionResult Index()
        {
            var employees = _employeeManager.GetEmployees();
            return View(employees);
        }

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
            return View();
        }

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
            return View(employee);
        }

        public JsonResult GetManagers(int deptId)
        {
            return Json(_employeeManager.GetManagers(deptId));
        }

        public IActionResult Edit(int? id)
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

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetDepartments(), "Id", "Name", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(_employeeManager.GetManagers(deptId: employee.DepartmentId), "Key", "Value", employee.ManagerId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,DepartmentId,Salary,IsManager,ManagerId,Phone,Email")] EmployeeBusinessModel employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _employeeManager.UpdateEmployee(id, employee);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetDepartments(), "Id", "Name", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(_employeeManager.GetManagers(deptId: employee.DepartmentId), "Key", "Value", employee.ManagerId);
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _employeeManager.DeleteEmployee((int)id);

            if (result == "Not found")
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
