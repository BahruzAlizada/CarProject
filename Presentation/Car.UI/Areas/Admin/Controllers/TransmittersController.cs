using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransmittersController : Controller
    {
        private readonly ITransmitterReadRepository transmitterReadRepository;
        private readonly ITransmitterWriteRepository transmitterWriteRepository;
        public TransmittersController(ITransmitterReadRepository transmitterReadRepository, ITransmitterWriteRepository transmitterWriteRepository)
        {
            this.transmitterReadRepository = transmitterReadRepository;
            this.transmitterWriteRepository = transmitterWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Transmitter> transmitters = transmitterReadRepository.GetAll();
            return View(transmitters);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Transmitter transmitter)
        {
            #region IsExist
            bool result = transmitterReadRepository.GetAll().Any(x => x.Name == transmitter.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            transmitterWriteRepository.Add(transmitter);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Transmitter transmitter = transmitterReadRepository.Get(x => x.Id == id);
            if (transmitter == null) return BadRequest();

            return View(transmitter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Transmitter newTransmitter)
        {
            if (id == null) return NotFound();
            Transmitter transmitter = transmitterReadRepository.Get(x => x.Id == id);
            if (transmitter == null) return BadRequest();

            #region IsExist
            bool result = transmitterReadRepository.GetAll().Any(x => x.Name == newTransmitter.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            transmitter.Name = newTransmitter.Name;

            transmitterWriteRepository.Update(transmitter);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Transmitter transmitter = transmitterReadRepository.Get(x => x.Id == id);
            if (transmitter == null) return BadRequest();

            transmitterWriteRepository.Activity(transmitter);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
