using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRM.Business.Models
{
    public class EmployeeBusinessModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z\\s*]+$", ErrorMessage = "Only alphabets are allowed")]
        public string Name { get; set; }

        public string Department { get; set; }

        [Required]
        [DisplayName("Department")]
        public byte DepartmentId { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public bool IsManager { get; set; }

        [DisplayName("Manager")]
        public int ManagerId { get; set; }

        public string Manager { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
