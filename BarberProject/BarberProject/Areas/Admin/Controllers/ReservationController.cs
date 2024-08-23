using BarberProject.ViewModels.Reservation;
using Domain.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _reservationService.GetAll();
            var reservations = datas.Select(m=> new ReservationVM { Id = m.Id, EmployeeName = m.Employee.BarberName, OrderStatus = m.OrderStatus.ToString(), Username = m.User.FullName}).ToList();
            return View(reservations);
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id is null) return BadRequest();
            var existReserv = await _reservationService.GetById((int)id);
            if (existReserv is null) return NotFound();

            existReserv.OrderStatus = OrderStatus.Accepted;

            await _reservationService.Edit((int)id, existReserv);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Reject(int? id)
        {
            if (id is null) return BadRequest();
            var existReserv = await _reservationService.GetById((int)id);
            if (existReserv is null) return NotFound();

            existReserv.OrderStatus = OrderStatus.Rejected;

            await _reservationService.Edit((int)id, existReserv);
            return RedirectToAction("Index");
        }
    }
}
