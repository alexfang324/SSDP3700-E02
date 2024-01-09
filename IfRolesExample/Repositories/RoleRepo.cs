using IfRolesExample.Data;
using IfRolesExample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IfRolesExample.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _db;

        public RoleRepo(ApplicationDbContext db)
        {
            this._db = db;
            CreateInitialRole();
        }

        public List<RoleVM> GetAllRoles()
        {
            var roles = _db.Roles.Select(r => new RoleVM
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToList();

            return roles;
        }

        public RoleVM GetRole(string roleName)
        {
            var role =
                _db.Roles.Where(r => r.Name == roleName)
                              .FirstOrDefault();

            if (role != null)
            {
                return new RoleVM()
                {
                    RoleName = role.Name
                                    ,
                    Id = role.Id
                };
            }
            return null;
        }


        public bool CreateRole(string roleName)
        {
            bool isSuccess = true;

            try
            {
                _db.Roles.Add(new IdentityRole
                {
                    Name = roleName,
                    Id = roleName,
                    NormalizedName = roleName.ToUpper()
                });
                _db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public void CreateInitialRole()
        {
            const string ADMIN = "Admin";

            var role = GetRole(ADMIN);

            if (role == null)
            {
                CreateRole(ADMIN);
            }
        }

        public string Delete(string id)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return "Role not found";
            }
            if (_db.UserRoles.Any(ur => ur.RoleId == role.Id))
            {
                return "Role is assigned to a user, cannot delete";
            }
            _db.Roles.Remove(role);
            _db.SaveChanges();
            return "Role deleted successfully";
        }

    }

}
