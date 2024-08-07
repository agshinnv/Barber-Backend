using Microsoft.AspNetCore.Mvc;

namespace BarberProject.Controllers
{
    public class ServicesController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
