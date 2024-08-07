using BarberProject.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IAboutService _aboutService;
        private readonly IHistoryService _historyService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISubscriberService _subscriberService;
        private readonly IBlogService _blogService;

        public HomeController(ISliderService sliderService,
                              IAboutService aboutService,
                              IHistoryService historyService,
                              IHttpContextAccessor accessor,
                              UserManager<AppUser> userManager,
                              ISubscriberService subscriberService,
                              IBlogService blogService)
        {
            _sliderService = sliderService;
            _aboutService = aboutService;
            _historyService = historyService;
            _accessor = accessor;
            _userManager = userManager;
            _subscriberService = subscriberService;
            _blogService = blogService;
        }
        
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();
            var histories = await _historyService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Abouts = abouts,
                History = histories.FirstOrDefault(),
                Blogs = blogs,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string subscriberEmail)
        {
            await _subscriberService.Create(new Subscriber { SubscriberEmail = subscriberEmail });
            return Ok();
        }
    }
}
