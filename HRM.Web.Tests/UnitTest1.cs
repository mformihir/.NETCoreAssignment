using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Business.Interface;
using HRM.Data.Models;
using HRM.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HRM.Web.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager.Setup(manager => manager.GetEmployees()).Returns()
            var mockDepartmentManager = new Mock<IDepartmentManager>();
            var mockUserManager = new Mock<UserManager<IdentityUser>>();
            var mockNotifyService = new Mock<INotyfService>();
            var controller = new EmployeesController(mockEmployeeManager.Object, mockDepartmentManager.Object, mockUserManager.Object, mockNotifyService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);


        }

        private static GetMockEmployees()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee
            {
                Name = "Mihir Joshi",
                Email = "mihir67mj@gmail.com",
                Salary = 23000,
                DepartmentId = 0,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                IsActive = true,
                IsManager = false,
                Phone = "9584351358"
            });
            employees.Add(new Employee
            {
                Name = "Vedanshu Joshi",
                Email = "test@test.com",
                Salary = 29000,
                DepartmentId = 1,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                IsActive = true,
                IsManager = false,
                Phone = "9584351358"
            });
        }
    }
}
