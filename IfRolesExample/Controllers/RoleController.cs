using IfRolesExample.Data;
using IfRolesExample.Models;
using IfRolesExample.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace IfRolesExample.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RoleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string message)
        {
            ViewData["Message"] = message;
            RoleRepo _roleRepo = new RoleRepo(_db);
            List<RoleVM> roleList = _roleRepo.GetAllRoles();
            return View(roleList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_db);
                bool isSuccess = roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Role creation failed.\n" +
                                             " The role already exist.");
                }
            }
            return View(roleVM);
        }

        public IActionResult Delete(string roleName)
        {
            RoleRepo roleRepo = new RoleRepo(_db);
            RoleVM viewModel = roleRepo.GetRole(roleName);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(RoleVM viewModel)
        {
            RoleRepo roleRepo = new RoleRepo(_db);
            string repoMessage = roleRepo.Delete(viewModel.Id);

            return RedirectToAction("Index", new { message = repoMessage });
        }

    }
}
