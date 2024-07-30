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

            List<AppointmentVM> model = datas.Select(m => new AppointmentVM { Id = m.Id, Title = m.Title, IconImage = m.IconImage }).ToList();

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

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existAppointment = await _appointmentService.GetById((int)id);
            if (existAppointment is null) return NotFound();

            AppointmentDetailVM model = new()
            {
                Title = existAppointment.Title,
                IconImage = existAppointment.IconImage,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existAppointment = await _appointmentService.GetById((int)id);
            if (existAppointment is null) return NotFound();

            await _appointmentService.Delete(existAppointment);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existAppointment = await _appointmentService.GetById((int)id);
            if (existAppointment is null) return NotFound();

            AppointmentEditVM model = new()
            {
                Title = existAppointment.Title,
                ExistIconImage = existAppointment.IconImage
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AppointmentEditVM request)
        {
            if(!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existAppointment = await _appointmentService.GetById((int)id);
            if (existAppointment is null) return NotFound();

            if (request.NewIconImage is not null)
            {
                if (!request.NewIconImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewIconImage", "File type must be image");
                    request.ExistIconImage = existAppointment.IconImage;
                    return View(request);
                }

                if (!request.NewIconImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewIconImage", "Image size must be less than 2 Mb");
                    request.ExistIconImage = existAppointment.IconImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existAppointment.IconImage);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewIconImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewIconImage.SaveFileToLocalAsync(path);
                await _appointmentService.Edit((int)id, new Appointment {Title = request.Title, IconImage = fileName });

            }
            else
            {
                await _appointmentService.Edit((int)id, new Appointment { Title = request.Title, IconImage = existAppointment.IconImage });
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
