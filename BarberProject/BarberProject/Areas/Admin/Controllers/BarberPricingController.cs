using BarberProject.ViewModels.BarberPrices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BarberPricingController : Controller
    {
        private readonly IBarberPricingService _barberPricingService;

        public BarberPricingController(IBarberPricingService barberPricingService)
        {
            _barberPricingService = barberPricingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _barberPricingService.GetAll();

            List<BarberPricingVM> model = datas.Select(m => new BarberPricingVM { Id = m.Id, ServiceName = m.ServiceName, Description = m.Description, Price = m.Price }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarberPricingCreateVM request)
        {
            if(!ModelState.IsValid) return View();

            await _barberPricingService.Create(new BarberPricing { ServiceName = request.ServiceName, Description = request.ServiceDescription, Price = request.ServicePrice });

            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if(existBarberPricing is null) return NotFound();

            await _barberPricingService.Delete(existBarberPricing);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if (existBarberPricing is null) return NotFound();

            BarberPricingDetailVM model = new()
            {
                ServiceName = existBarberPricing.ServiceName,
                ServiceDescription = existBarberPricing.Description,
                ServicePrice = existBarberPricing.Price
            };

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if (existBarberPricing is null) return NotFound();

            BarberPricingEditVM model = new()
            {
                ServiceName = existBarberPricing.ServiceName,
                ServiceDescription = existBarberPricing.Description,
                ServicePrice = existBarberPricing.Price
            };
            
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BarberPricingEditVM request)
        {
            if(!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if (existBarberPricing is null) return NotFound();

            await _barberPricingService.Edit((int)id, new BarberPricing { ServiceName = request.ServiceName, Description = request.ServiceDescription, Price = request.ServicePrice });

            return RedirectToAction(nameof(Index));


        }

    }
}
