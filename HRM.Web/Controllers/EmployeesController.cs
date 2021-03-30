using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRM.Business.Interface;
using HRM.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HRM.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyf;

        public EmployeesController(IEmployeeManager employeeManager, IDepartmentManager departmentManager, UserManager<IdentityUser> userManager, INotyfService notyf)
        {
            _employeeManager = employeeManager;
            _departmentManager = departmentManager;
            _userManager = userManager;
            _notyf = notyf;
        }

        /// <summary>
        /// Renders a List of all the Employees
        /// </summary>
        public IActionResult Index()
        {
            var employees = _employeeManager.GetEmployees();
            return View(employees);
        }

        /// <summary>
        /// Renders Employee Detail page
        /// </summary>
        /// <param name="id">ID of Employee</param>
        /// <returns></returns>
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

        /// <summary>
        /// Renders the Employee Create Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_departmentManager.GetDepartments(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Creates the Employee and Redirects to Employee List
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,DepartmentId,Salary,IsManager,ManagerId,Phone,Email")] EmployeeBusinessModel employee)
        {
            if (ModelState.IsValid)
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = _employeeManager.CreateEmployee(employee, loggedInUserId);
                if (result == "Success")
                {
                    _notyf.Success("Success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Custom(result, 5, "#DFC52C", "fa fa-exclamation-triangle");
                    return RedirectToAction("Error", "Home");
                }
            }
            ViewData["DepartmentId"] = new SelectList(_departmentManager.GetDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        /// <summary>
        /// Gets the managers of the specified ID from Database (used for Dropdown)
        /// </summary>
        /// <param name="deptId">Department ID of managers</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetManagers(int deptId)
        {
            return Json(_employeeManager.GetManagers(deptId));
        }

        /// <summary>
        /// Renders Employee Edit View
        /// </summary>
        /// <param name="id">ID of Employee</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the Employee into the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
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
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = _employeeManager.UpdateEmployee(id, employee, loggedInUserId);
                if (result == "Success")
                {
                    _notyf.Success(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Custom(result, 5, "#DFC52C", "fa fa-exclamation-triangle");
                    return RedirectToAction("Error", "Home");
                }
            }

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetDepartments(), "Id", "Name", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(_employeeManager.GetManagers(deptId: employee.DepartmentId), "Key", "Value", employee.ManagerId);
            return View(employee);
        }

        /// <summary>
        /// Deletes the specified Employee from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _employeeManager.DeleteEmployee((int)id);

            if (result == "Not found")
            {
                _notyf.Custom(result, 5, "#DFC52C", "fa fa-exclamation-triangle");
                return NotFound();
            }
            else if (result == "Success")
            {
                _notyf.Success(result);
            }
            else
            {
                _notyf.Custom(result, 5, "#DFC52C", "fa fa-exclamation-triangle");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
