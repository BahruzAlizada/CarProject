using Car.Application.ViewModels;
using Car.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        public RolesController(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppRole> roles = await roleManager.Roles.ToListAsync();
            List<RoleVM> rolesVM = new List<RoleVM>();

            foreach (var item in roles)
            {
                RoleVM vm = new RoleVM
                {
                    Name = item.Name
                };
                rolesVM.Add(vm);
            }
            return View(rolesVM);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoleVM role)
        {
            AppRole appRole = new AppRole
            {
                Name = role.Name,
                NormalizedName = role.ToString().ToUpper(),
            };

            await roleManager.CreateAsync(appRole);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
