using Car.Application.ViewModels;
using Car.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public AdminsController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppUser> admins = await userManager.Users.Where(x => !x.UserRole.Equals("Company") && !x.UserRole.Equals("User")).ToListAsync();
            List<AdminVM> adminVMs = new List<AdminVM>();

            foreach (var item in admins)
            {
                AdminVM vm = new AdminVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Username = item.UserName,
                    Email = item.Email,
                    Created = item.Created,
                    Status = item.Status,
                    Role = (await userManager.GetRolesAsync(item))[0]
                };
                adminVMs.Add(vm);
            }
            return View(adminVMs);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Equals("Company") && !x.Name.Equals("User")).Select(x=>x.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AdminVM adminVM, string roleName)
        {
            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Equals("Company") && !x.Name.Equals("User")).Select(x => x.Name).ToListAsync();
            AppRole? role = await roleManager.FindByNameAsync(roleName);
            if (role == null) return BadRequest();

            AppUser admin = new AppUser
            {
                Name = adminVM.Name,
                Email = adminVM.Email,
                UserName = Guid.NewGuid().ToString("N").Substring(0, 8),
                UserRole = role.Name,
            };

            IdentityResult result = await userManager.CreateAsync(admin, adminVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await userManager.AddToRoleAsync(admin, role.Name);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            AdminVM vm = new AdminVM
            {
                Id = admin.Id,
                Email = admin.Email,
                Name = admin.Name,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(Guid? id, AdminVM newVM)
        {
            #region Get
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            AdminVM vm = new AdminVM
            {
                Id = admin.Id,
                Email = admin.Email,
                Name = admin.Name,
            };
            #endregion

            admin.Id = newVM.Id;
            admin.Name = newVM.Name;
            admin.Email = newVM.Email;

            await userManager.UpdateAsync(admin);
            return RedirectToAction("Index");
        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(Guid? id, ResetPasswordVM resetPassword)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            string token = await userManager.GeneratePasswordResetTokenAsync(admin);

            IdentityResult result = await userManager.ResetPasswordAsync(admin, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region RoleChange
        public async Task<IActionResult> RoleChange(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company") && !x.Name.Contains("User")).Select(x => x.Name).ToListAsync();

            RoleVM roleVM = new RoleVM
            {
                Name = (await userManager.GetRolesAsync(admin))[0]
            };

            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RoleChange(Guid? id, string role)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company") && !x.Name.Contains("User")).Select(x => x.Name).ToListAsync();

            RoleVM roleVM = new RoleVM
            {
                Name = (await userManager.GetRolesAsync(admin))[0]
            };

            await userManager.RemoveFromRoleAsync(admin, roleVM.Name);
            await userManager.AddToRoleAsync(admin, role);

            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            if (admin.Status)
                admin.Status = false;
            else
                admin.Status = true;

            await userManager.UpdateAsync(admin);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? admin = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (admin == null) return BadRequest();

            await userManager.DeleteAsync(admin);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
