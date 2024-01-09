﻿using IfRolesExample.Data;
using IfRolesExample.Models;
using IfRolesExample.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IfRolesExample.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(ApplicationDbContext db
                                 , UserManager<IdentityUser> userManager
                                 , RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Index()
        {
            UserRepo userRepo = new UserRepo(_db);
            var users = userRepo.GetAllEmails();
            return View(users);
        }

        // Show all roles for a specific user.
        public async Task<IActionResult> Detail(string userName, string message)
        {
            ViewBag.Message = message;
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            MyRegisteredUserRepo registeredUserRepo = new MyRegisteredUserRepo(_db);
            var viewModel = await userRoleRepo.GetUserRolesAsync(userName);
            string userFullName = registeredUserRepo.GetUsernameFromEmail(userName);

            ViewBag.UserName = userFullName;
            ViewBag.Email = userName;

            return View(viewModel);
        }

        // Present user with ability to assign roles to a user.
        // It gives two drop downs - the first contains the user names with
        // the requested user selected. The second drop down contains all
        // possible roles.
        public ActionResult Create(string userName)
        {
            // Store the email address of the Identity user
            // which is their user name.
            ViewBag.SelectedUser = userName;

            // Build SelectList with role data and store in ViewBag.
            RoleRepo roleRepo = new RoleRepo(_db);
            var roles = roleRepo.GetAllRoles().ToList();

            // There might be a better way but I have always found using the 
            // .NET dropdown lists to be a challenge. Here is a way to make 
            // it work if you can get the data in the proper format. 

            // 1. Preparation for 'Roles' drop down.
            //    a) Build a list of SelectListItem objects which have 'Value' and 
            //       'Text' properties. 
            var preRoleList = roles.Select(r =>
                new SelectListItem { Value = r.RoleName, Text = r.RoleName })
                    .ToList();
            //    b) Store the SelectListItem objects in a SelectList object 
            //       with 'Value' and 'Text' properties set specifically.
            var roleList = new SelectList(preRoleList, "Value", "Text");

            //    c) Store the SelectList in a ViewBag.
            ViewBag.RoleSelectList = roleList;

            // 2. Preparation for 'Users' drop down list. 
            //    a) Build a list of SelectListItem objects which have 'Value' and 
            //       'Text' properties.
            var userList = _db.Users.ToList();

            //    b) Store the SelectListItem objects in a SelectList object 
            //       with 'Value' and 'Text' properties set specifically.
            var preUserList = userList.Select(u =>
                new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            SelectList userSelectList = new SelectList(preUserList
                                                        , "Value"
                                                        , "Text");

            //    c) Store the SelectList in a ViewBag.
            ViewBag.UserSelectList = userSelectList;
            return View();
        }

        // Assigns role to user.
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);

            if (ModelState.IsValid)
            {
                var addUR = await userRoleRepo.AddUserRoleAsync(userRoleVM.Email
                                                               , userRoleVM.Role);
            }
            try
            {
                return RedirectToAction("Detail", "UserRole",
                        new { userName = userRoleVM.Email });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(string id, string email, string roleName)
        {
            UserRoleVM viewModel = new UserRoleVM() { Email = email, Role = roleName };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserRoleVM viewModel)
        {
            string message;
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            bool success = await userRoleRepo.RemoveUserRoleAsync(viewModel.Email, viewModel.Role);

            if (success)
            {
                message = "Role deleted sucessfully";
            }
            else { message = "An error occured while deleting role"; }

            return RedirectToAction("Detail", new { username = viewModel.Email, message = message });
        }

    }
}