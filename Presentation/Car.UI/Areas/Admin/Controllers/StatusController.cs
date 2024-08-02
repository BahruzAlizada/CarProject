using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusController : Controller
    {
        private readonly IStatusReadRepository statusReadRepository;
        private readonly IStatusWriteRepository statusWriteRepository;
        public StatusController(IStatusReadRepository statusReadRepository, IStatusWriteRepository statusWriteRepository)
        {
            this.statusReadRepository = statusReadRepository;
            this.statusWriteRepository = statusWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Status> statuses = statusReadRepository.GetAll();
            return View(statuses);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Status status)
        {
            statusWriteRepository.Add(status);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Status sttatus = statusReadRepository.Get(x => x.Id == id);
            if (sttatus == null) return BadRequest();

            return View(sttatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Status newStatus)
        {
            if (id == null) return NotFound();
            Status sttatus = statusReadRepository.Get(x => x.Id == id);
            if (sttatus == null) return BadRequest();
            

            sttatus.Name = newStatus.Name;

            statusWriteRepository.Update(sttatus);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Status sttatus = statusReadRepository.Get(x => x.Id == id);
            if (sttatus == null) return BadRequest();

            statusWriteRepository.Activity(sttatus);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
