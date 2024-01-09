using IfRolesExample.Data;
using IfRolesExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IfRolesExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult SecureArea()
        {
            string userName = User.Identity.Name;
            var registeredUser = _db.MyRegisteredUsers.FirstOrDefault(ru => ru.Email == userName);

            return View(registeredUser);
        }

    }
}