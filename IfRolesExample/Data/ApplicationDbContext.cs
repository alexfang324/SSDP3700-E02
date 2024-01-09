using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IfRolesExample.Models;

namespace IfRolesExample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }
        public DbSet<IfRolesExample.Models.RoleVM> RoleVM { get; set; } = default!;
        public DbSet<IfRolesExample.Models.UserVM> UserVM { get; set; } = default!;
        public DbSet<IfRolesExample.Models.UserRoleVM> UserRoleVM { get; set; } = default!;
    }
}