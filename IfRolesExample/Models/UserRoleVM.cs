using System.ComponentModel.DataAnnotations;

namespace IfRolesExample.Models
{
    public class UserRoleVM
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? Role { get; set; }
    }
}
