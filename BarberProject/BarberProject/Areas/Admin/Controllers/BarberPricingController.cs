﻿using BarberProject.ViewModels.BarberPrices;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class BarberPricingController : Controller
    {
        private readonly IBarberPricingService _barberPricingService;
        private readonly IPricingCategoryService _priceCategoryService;
        public BarberPricingController(IBarberPricingService barberPricingService, 
                                       IPricingCategoryService priceCategoryService)
        {
            _barberPricingService = barberPricingService;
            _priceCategoryService = priceCategoryService;
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
            var pricingCategories = await _priceCategoryService.GetAll();

            ViewBag.pricingCategories = new SelectList(pricingCategories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarberPricingCreateVM request)
        {
            var pricingCategories = await _priceCategoryService.GetAll();

            ViewBag.pricingCategories = new SelectList(pricingCategories, "Id", "Name");

            if (!ModelState.IsValid) return View();

            await _barberPricingService.Create(new BarberPricing { ServiceName = request.ServiceName, Description = request.ServiceDescription, Price = request.ServicePrice, PricingCategoryId = request.PricingCategoryId });

            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
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
                ServicePrice = existBarberPricing.Price,
                PricingCategory = existBarberPricing.PricingCategory.Name
            };

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var pricingCategories = await _priceCategoryService.GetAll();

            ViewBag.pricingCategories = new SelectList(pricingCategories, "Id", "Name");

            if (id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if (existBarberPricing is null) return NotFound();

            BarberPricingEditVM model = new()
            {
                ServiceName = existBarberPricing.ServiceName,
                ServiceDescription = existBarberPricing.Description,
                ServicePrice = existBarberPricing.Price,
                PricingCategoryId = existBarberPricing.PricingCategoryId,
            };
            
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BarberPricingEditVM request)
        {
            var pricingCategories = await _priceCategoryService.GetAll();

            ViewBag.pricingCategories = new SelectList(pricingCategories, "Id", "Name");

            if (!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existBarberPricing = await _barberPricingService.GetById((int)id);
            if (existBarberPricing is null) return NotFound();

            await _barberPricingService.Edit((int)id, new BarberPricing { ServiceName = request.ServiceName, Description = request.ServiceDescription, Price = request.ServicePrice, PricingCategoryId = request.PricingCategoryId });

            return RedirectToAction(nameof(Index));


        }

    }
}
