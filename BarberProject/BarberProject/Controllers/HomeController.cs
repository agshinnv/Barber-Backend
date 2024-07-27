using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders
            };

            return View(model);
        }
    }
}
