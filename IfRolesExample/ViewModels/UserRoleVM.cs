using System.ComponentModel.DataAnnotations;

namespace IfRolesExample.ViewModels
{
    public class UserRoleVM
    {
        //[Key]
        //[Display(Name = "ID")]
        //public int? ID { get; set; }

        [Key]
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? RoleName { get; set; }
    }
}
