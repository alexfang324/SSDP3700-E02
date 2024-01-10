using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IfRolesExample.Models;
using IfRolesExample.ViewModels;

namespace IfRolesExample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }
        public DbSet<RoleVM> RoleVM { get; set; } = default!;
        public DbSet<UserVM> UserVM { get; set; } = default!;
        public DbSet<UserRoleVM> UserRoleVM { get; set; } = default!;
    }
}