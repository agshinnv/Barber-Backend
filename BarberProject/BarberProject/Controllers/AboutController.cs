using Microsoft.AspNetCore.Mvc;

namespace BarberProject.Controllers
{
    public class AboutController : Controller
    {



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
