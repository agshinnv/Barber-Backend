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
        private readonly IHistoryService _historyService;

        public HomeController(ISliderService sliderService,
                              IAboutService aboutService,
                              IHistoryService historyService)
        {
            _sliderService = sliderService;
            _aboutService = aboutService;
            _historyService = historyService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();
            var histories = await _historyService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Abouts = abouts,
                History = histories.FirstOrDefault()
            };

            return View(model);
        }
    }
}
