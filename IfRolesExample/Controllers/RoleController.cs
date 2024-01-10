﻿using IfRolesExample.Data;
using IfRolesExample.Repositories;
using IfRolesExample.ViewModels;
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

        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            RoleRepo roleRepo = new RoleRepo(_db);
            return View(roleRepo.GetAllRoles());
        }

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
                bool isSuccess =
                    roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    return RedirectToAction("Index", new { message = "Role Created Successfully" });
                }
                else
                {
                    ModelState
                    .AddModelError("", "Role creation failed.");
                    ModelState
                    .AddModelError("", "The role may already" +
                                       " exist.");
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
            bool isRoleAssigned = roleRepo.isRoleAssigned(viewModel.RoleName);
            string repoMessage = "";
            if (!isRoleAssigned)
            {
                repoMessage = roleRepo.Delete(viewModel.RoleName);
                return RedirectToAction("Index", new { message = repoMessage });
            }
            else
            {
                repoMessage = "The role is assigned to a user. Please detach this role from all users first";
                {
                    ModelState
                    .AddModelError("", "Role deletion failed.");
                    ModelState
                    .AddModelError("", "The role is assigned to a user. Please detach this role from all users first");
                }
            }
            return View(viewModel);

        }

    }
}
