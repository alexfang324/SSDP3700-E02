﻿using System.ComponentModel.DataAnnotations;

namespace IfRolesExample.ViewModels
{
    public class RoleVM
    {
        //[Display(Name = "ID")]
        //public string? Id { get; set; }

        [Key]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

}
