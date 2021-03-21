using System;
using System.Collections.Generic;

#nullable disable

namespace HRM.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public bool IsManager { get; set; }
        public int? ManagerId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
