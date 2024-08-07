using Microsoft.AspNetCore.Mvc;

namespace BarberProject.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
