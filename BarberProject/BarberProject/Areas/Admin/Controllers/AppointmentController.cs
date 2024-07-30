using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Appointments;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IWebHostEnvironment _env;

        public AppointmentController(IAppointmentService appointmentService,
                                     IWebHostEnvironment env)
        {
            _appointmentService = appointmentService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _appointmentService.GetAll();

            List<AppointmentVM> model = datas.Select(m => new AppointmentVM { Id = m.Id, Title = m.Title }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.IconImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("IconImage", "File type must be image");
                return View();
            }

            if (!request.IconImage.CheckFileSize(2))
            {
                ModelState.AddModelError("IconImage", "Image size must be less than 2 Mb");
                return View();
            }


            
            string fileName = Guid.NewGuid().ToString() + "-" + request.IconImage.FileName;
            string path = Path.Combine(_env.WebRootPath, "images", fileName);
            await request.IconImage.SaveFileToLocalAsync(path);


            await _appointmentService.Create(new Appointment { Title = request.Title, IconImage = fileName });

            return RedirectToAction(nameof(Index));

        }
    }
}
