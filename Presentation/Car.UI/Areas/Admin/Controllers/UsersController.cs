using Car.Application.Abstract;
using Car.Application.ViewModels;
using Car.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ICarReadRepository carReadRepository;
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ICarReadRepository carReadRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.carReadRepository = carReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            double PageCount = await userManager.Users.Where(x => x.UserRole.Contains("User")).CountAsync();

            ViewBag.PageCount = Math.Ceiling(PageCount / 20);
            ViewBag.CurrentPage = page;

            List<AppUser> users = await userManager.Users.Where(x => x.UserRole.Contains("User") && (x.Name.Contains(search) || search == null)).OrderByDescending(x => x.Created).
                Skip((page - 1) * 20).Take(20).ToListAsync();
            List<UserVM> userVMs = new List<UserVM>();

            foreach (var item in users)
            {
                UserVM vm = new UserVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    UserName = item.UserName,
                    Status = item.Status,
                    Created = item.Created,
                    CarCount = await carReadRepository.GetCountAsync(x => x.UserId == item.Id)
                };
                userVMs.Add(vm);
            }

            return View(userVMs);
        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(Guid? id, ResetPasswordVM resetPassword)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
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

        #region Activity
        public async Task<IActionResult> Activity(Guid? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            if (user.Status)
                user.Status = false;
            else
                user.Status = true;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
