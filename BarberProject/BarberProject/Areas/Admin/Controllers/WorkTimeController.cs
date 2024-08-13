using BarberProject.ViewModels.WorkTimes;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class WorkTimeController : Controller
    {
        private readonly IWorkTimeService _workTimeService;

        public WorkTimeController(IWorkTimeService workTimeService)
        {
            _workTimeService = workTimeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _workTimeService.GetAll();

            List<WorkTimeVM> model = datas.Select(m=> new WorkTimeVM { Id = m.Id, WorkDay = m.WorkDay, WorkHour = m.WorkHour }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(WorkTimeCreateVM request)
        {
            if(!ModelState.IsValid) return View();

            await _workTimeService.Create(new WorkTime { WorkDay = request.WorkDay, WorkHour = request.WorkHour });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existWorkDay = await _workTimeService.GetById((int)id);

            if(existWorkDay is null) return NotFound();

            await _workTimeService.Delete(existWorkDay);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existWorkDay = await _workTimeService.GetById((int)id);

            if (existWorkDay is null) return NotFound();

            WorkTimeEditVM model = new()
            {
                WorkDay = existWorkDay.WorkDay,
                WorkHour = existWorkDay.WorkHour,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,WorkTimeEditVM request)
        {
            if(!ModelState.IsValid) return View();

            if (id is null) return BadRequest();

            var existWorkDay = await _workTimeService.GetById((int)id);

            if (existWorkDay is null) return NotFound();

            await _workTimeService.Edit((int)id, new WorkTime { WorkDay = request.WorkDay, WorkHour = request.WorkHour });

            return RedirectToAction(nameof(Index));

        }

        
    }
}
