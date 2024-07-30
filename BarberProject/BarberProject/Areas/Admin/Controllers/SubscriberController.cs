using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subscribers = await _subscriberService.GetAll();
            return View(subscribers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var subscriber = await _subscriberService.GetById((int)id);
            if (subscriber is null) return NotFound();

            await _subscriberService.Delete(subscriber);
            return RedirectToAction(nameof(Index));
        }
    }
}
