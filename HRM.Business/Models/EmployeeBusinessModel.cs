using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HRM.Business.Models
{
    public class EmployeeBusinessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        [DisplayName("Department")]
        public byte DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public bool IsManager { get; set; }

        [DisplayName("Manager")]
        public int ManagerId { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
