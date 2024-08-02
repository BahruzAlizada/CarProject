using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeatNumbersController : Controller
    {
        public readonly ISeatReadRepository seatReadRepository;
        private readonly ISeatWriteRepository seatWriteRepository;
        public SeatNumbersController(ISeatReadRepository seatReadRepository,ISeatWriteRepository seatWriteRepository)
        {
            this.seatReadRepository = seatReadRepository;
            this.seatWriteRepository = seatWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Seat> seats = seatReadRepository.GetAll();
            return View(seats);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Seat seat)
        {
            seatWriteRepository.Add(seat);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Seat seat = seatReadRepository.Get(x => x.Id == id);
            if (seat == null) return BadRequest();

            return View(seat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Seat newSeat)
        {
            if (id == null) return NotFound();
            Seat seat = seatReadRepository.Get(x => x.Id == id);
            if (seat == null) return BadRequest();
           


            seat.SeatNumber = seat.SeatNumber;

            seatWriteRepository.Update(seat);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Seat seat = seatReadRepository.Get(x => x.Id == id);
            if (seat == null) return BadRequest();

            seatWriteRepository.Activity(seat);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
