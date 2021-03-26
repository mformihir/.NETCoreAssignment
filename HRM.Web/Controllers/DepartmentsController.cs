using HRM.Business.Interface;
using HRM.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentManager _departmentManager;
        public DepartmentsController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        /// <summary>
        /// Renders a List of all the Departments
        /// </summary>
        public IActionResult Index()
        {
            var departments = _departmentManager.GetDepartments();
            return View(departments);
        }

        /// <summary>
        /// Renders the Department Create Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the Department and Redirects to Employee List
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] DepartmentBusinessModel department)
        {
            if (ModelState.IsValid)
            {
                var result = _departmentManager.CreateDepartment(department);
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Error", "Home");
            }
            return View(department);
        }
    }
}
