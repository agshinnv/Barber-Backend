using Microsoft.AspNetCore.Mvc;

namespace BarberProject.Controllers
{
    public class PricingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
