using System.ComponentModel.DataAnnotations;

namespace HRM.Business.Models
{
    public class DepartmentBusinessModel
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z\\s*]+$", ErrorMessage = "Only alphabets are allowed. Length should be 50 (max).")]
        public string Name { get; set; }
    }
}
