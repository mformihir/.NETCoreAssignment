using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Business.Interface;
using HRM.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentManager _departmentManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyf;
        public DepartmentsController(IDepartmentManager departmentManager, UserManager<IdentityUser> userManager, INotyfService notyf)
        {
            _departmentManager = departmentManager;
            _userManager = userManager;
            _notyf = notyf;
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
        /// Creates the Department and Redirects to Department List
        /// </summary>
        /// <param name="department"></param>
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
                    _notyf.Success("Success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Custom(result, 5, "#DFC52C", "fa fa-exclamation-triangle");
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(department);
        }

        /// <summary>
        /// Renders Department Detail page
        /// </summary>
        /// <param name="id">ID of Department</param>
        /// <returns></returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentManager.GetDepartment((int)id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        /// <summary>
        /// Renders Department Edit View
        /// </summary>
        /// <param name="id">ID of Department</param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentManager.GetDepartment((int)id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


        /// <summary>
        /// Updates the Department into the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] DepartmentBusinessModel department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = _departmentManager.UpdateDepartment(id, department, loggedInUserId);
                if (result == "Success")
                {
                    _notyf.Success(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Custom(result, 5, "red", "fa fa-gear");
                    //_notyf.Error(result);
                    return RedirectToAction("Error", "Home");
                }
            }

            return View(department);
        }

        /// <summary>
        /// Deletes the specified Department from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _departmentManager.DeleteDepartment((int)id);

            if (result == "Not found")
            {
                _notyf.Error(result);
                return NotFound();
            }
            else if (result == "Success")
            {
                _notyf.Success(result);
            }
            else
            {
                _notyf.Custom(result, 5, "whitesmoke", "fa fa-gear");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
