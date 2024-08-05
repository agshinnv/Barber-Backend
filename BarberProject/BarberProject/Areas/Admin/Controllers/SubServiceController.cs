using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Employees;
using BarberProject.ViewModels.SubServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubServiceController : Controller
    {
        private readonly ISubServiceService _subServiceService;
        private readonly IServiceService _serviceService;

        public SubServiceController(ISubServiceService subServiceService,
                                    IServiceService serviceService)
        {
            _subServiceService = subServiceService;
            _serviceService = serviceService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _subServiceService.GetAll();

            List<SubServiceVM> model = datas.Select(m => new SubServiceVM { Id = m.Id, SubServiceName = m.ServiceName, SubServicePrice = m.ServicePrice }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubServiceCreateVM request)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (!ModelState.IsValid) return View();

            

            SubService subService = new()
            {
                ServiceName = request.SubServiceName,
                ServicePrice = request.SubServicePrice,
                ServiceId = request.ServiceId,
            };

            await _subServiceService.Create(subService);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var subService = await _subServiceService.GetById((int)id);
            if (subService is null) return NotFound();

            SubServiceDetailVM model = new()
            {
                Name = subService.ServiceName,
                Price = subService.ServicePrice,
                Service = subService.Service.Title
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existSubService = await _subServiceService.GetById((int)id);
            if (existSubService is null) return NotFound();


            await _subServiceService.Delete(existSubService);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (id is null) return BadRequest();
            var existSubService = await _subServiceService.GetById((int)id);
            if (existSubService is null) return NotFound();

            SubServiceEditVM model = new()
            {
                SubServiceName = existSubService.ServiceName,
                SubServicePrice = existSubService.ServicePrice,
                ServiceId = existSubService.ServiceId,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SubServiceEditVM request)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (id is null) return BadRequest();
            var existSubService = await _subServiceService.GetById((int)id);
            if (existSubService is null) return NotFound();

            if (!ModelState.IsValid) return View();

            await _subServiceService.Edit((int)id, new SubService { ServiceName = request.SubServiceName,ServicePrice = request.SubServicePrice, ServiceId = request.ServiceId });

            return RedirectToAction(nameof(Index));
        }
    }
}
