using BarberProject.Helpers;
using BarberProject.ViewModels.Reservation;
using Domain.Helpers.Enums;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using Service.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Domain.Models;
using Microsoft.Extensions.Options;
using BarberProject.ViewModels.Histories;
using Service.Services;
using BarberProject.Helpers.Extentions;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(IReservationService reservationService, IOptions<AppSettings> appSettings,UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _reservationService.GetAll();
            var reservations = datas.Select(m=> new ReservationVM {Id = m.Id, EmployeeName = m.Employee.BarberName, OrderStatus = m.OrderStatus.ToString(), Username = m.User.FullName}).ToList();
            return View(reservations);
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id is null) return BadRequest();
            var existReserv = await _reservationService.GetById((int)id);
            if (existReserv is null) return NotFound();

            existReserv.OrderStatus = OrderStatus.Accepted;
            var existUser = await _userManager.FindByIdAsync(existReserv.UserId);



            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/reservation.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{Username}", "Reservation accepted");

            string subject = "Reservation accepted";

            SendEmail(existUser.Email, subject, html);

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

            var existUser = await _userManager.FindByIdAsync(existReserv.UserId);



            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/rejectreservation.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{Username}", "Reservation rejected");

            string subject = "Reservation rejected";

            SendEmail(existUser.Email, subject, html);


            await _reservationService.Edit((int)id, existReserv);
            return RedirectToAction("Index");
        }

        public void SendEmail(string to, string subject, string html, string from = null)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _appSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.Server, _appSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.Username, _appSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existReservation = await _reservationService.GetByIdWithIncludes((int)id);
            if (existReservation is null) return NotFound();
            ReservationDetailVM model = new()
            {
                Username = existReservation.User.FullName,
                EmployeeName = existReservation.Employee.BarberName,
                OrderStatus = existReservation.OrderStatus.ToString(),
                Time = existReservation.Time.ToString(),
                ServiceName = existReservation.Service.Title,
                UserId = existReservation.UserId,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existReservation = await _reservationService.GetById((int)id);
            if (existReservation is null) return NotFound();

            await _reservationService.Delete(existReservation);

            return RedirectToAction(nameof(Index));

        }
    }
}
