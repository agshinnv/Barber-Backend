using BarberProject.ViewModels.Positions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _positionService.GetAll();

            List<PositionVM> model = datas.Select(m=> new PositionVM { Id = m.Id, PositionName = m.Name }).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionCreateVM request)
        {
            if(!ModelState.IsValid) return View();

            await _positionService.Create(new Position { Name = request.PositionName });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existPosition = await _positionService.GetById((int)id);

            if(existPosition is null) return NotFound();

            await _positionService.Delete(existPosition);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existPosition = await _positionService.GetById((int)id);

            if (existPosition is null) return NotFound();

            PositionEditVM model = new()
            {
                PositionName = existPosition.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PositionEditVM request)
        {
            if (!ModelState.IsValid) return View();

            if (id is null) return BadRequest();

            var existPosition = await _positionService.GetById((int)id);

            if (existPosition is null) return NotFound();

            await _positionService.Edit((int)id, new Position { Name = request.PositionName });

            return RedirectToAction(nameof(Index));

        }
    }
}
