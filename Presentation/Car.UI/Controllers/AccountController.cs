using Car.Application.ViewModels;
using Car.Domain.Identity;
using Car.Infrastructure.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Car.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IWebHostEnvironment env;
        private readonly IPhotoService photoService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,
            IWebHostEnvironment env,IPhotoService photoService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.env = env;
            this.photoService = photoService;   
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser? user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email yaxud Şifrə yanlışdır");
                return View();
            }
            if (!user.Status)
            {
                ModelState.AddModelError("", "Sizin hesabınız admin tərəfdən bloklanıb");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.IsRemember, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email yaxud şifrə yanlışdır");
                return View();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Sizin hesabınız qısa müddətlik bloklanıb. 1 dəqiqə sonra yenidən yoxlayın");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region CompanyRegister
        public IActionResult CompanyRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CompanyRegister(RegisterVM register)
        {
            AppUser company = new AppUser
            {
                Name = register.Name,
                Email = register.Email,
                UserName = Guid.NewGuid().ToString("N").Substring(0, 8),
                UserRole = "Company",
                Seen = 0,
                IsCompany = true,
            };

            #region Image
            (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(register.Photo, false, false);
            if (!isValid)
            {
                ModelState.AddModelError("Photo", errorMessage);
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "company");
            company.Image = await photoService.SavePhotoAsync(register.Photo, folder);
            #endregion


            IdentityResult result = await userManager.CreateAsync(company, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var roleName = await roleManager.FindByNameAsync("Company");
            await userManager.AddToRoleAsync(company, roleName.Name);
            await signInManager.SignInAsync(company, register.IsRemember);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region UserRegister
        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UserRegister(RegisterVM register)
        {
            AppUser user = new AppUser
            {
                Name = register.Name,
                Email = register.Email,
                UserName = Guid.NewGuid().ToString("N").Substring(0, 8),
                UserRole = "User",
                IsCompany = false,
            };


            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var roleName = await roleManager.FindByNameAsync("User");
            await userManager.AddToRoleAsync(user, roleName.Name);
            await signInManager.SignInAsync(user, register.IsRemember);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
