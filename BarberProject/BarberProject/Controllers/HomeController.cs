using BarberProject.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IAboutService _aboutService;

        public HomeController(ISliderService sliderService,
                              IAboutService aboutService)
        {
            _sliderService = sliderService;
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Abouts = abouts
            };

            return View(model);
        }
    }
}
