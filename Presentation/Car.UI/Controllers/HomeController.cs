using Car.Application.Abstract;
using Car.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Car.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarkaReadRepository markaReadRepository;
        private readonly IModelReadRepository modelReadRepository;
        private readonly IBanReadRepository banReadRepository;
        private readonly ICityReadRepository cityReadRepository;
        private readonly IYearReadRepository yearReadRepository;

        public HomeController(IMarkaReadRepository markaReadRepository, IModelReadRepository modelReadRepository, IBanReadRepository banReadRepository,
            ICityReadRepository cityReadRepository,IYearReadRepository yearReadRepository)
        {
            this.markaReadRepository = markaReadRepository;
            this.modelReadRepository = modelReadRepository;
            this.banReadRepository = banReadRepository;
            this.cityReadRepository = cityReadRepository;
            this.yearReadRepository = yearReadRepository;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Markas = await markaReadRepository.GetAllAsync(x => x.IsMain && x.Status),
                Models = await modelReadRepository.GetAllAsync(x => !x.IsMain && x.Status),
                Bans = await banReadRepository.GetAllAsync(x => x.Status),
                Cities = await cityReadRepository.GetAllAsync(x => x.Status),
                Years  =await yearReadRepository.GetAllAsync(x=>x.Status)
            };
            return View(homeVM);
        }

    }
}
