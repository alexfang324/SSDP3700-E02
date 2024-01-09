using IfRolesExample.Data;
using IfRolesExample.Models;
using Microsoft.EntityFrameworkCore;

namespace IfRolesExample.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _db;
        public UserRepo(ApplicationDbContext db)
        {
            this._db = db;
        }

        public List<UserVM> GetAllEmails()
        {
            return _db.Users.Select(u => new UserVM { Email = u.Email }).ToList();

        }
    }
}
