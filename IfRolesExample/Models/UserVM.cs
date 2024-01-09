using System.ComponentModel.DataAnnotations;

namespace IfRolesExample.Models
{
    public class UserVM
    {
        [Key]
        [Display(Name = "Email")]
        public string? Email { get; set; }

    }
}
