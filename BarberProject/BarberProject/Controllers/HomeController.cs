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
        private readonly IServiceService _serviceService;
        private readonly IBarberService _barberService;
        private readonly IFeatureService _featureService;
        private readonly IPricingCategoryService _priceCategoryService;
        private readonly IEmployeeService _employeeService;
        private readonly IAppointmentService _appointmentService;
        private readonly ICommentService _commentService;
        private readonly IBarberPricingService _barberPricingService;
        private readonly IPositionService _positionService;
        private readonly ISettingService _settingService;
        private readonly IColleagueService _colleagueService;

        public HomeController(ISliderService sliderService,
                              IAboutService aboutService,
                              IHistoryService historyService,
                              IHttpContextAccessor accessor,
                              UserManager<AppUser> userManager,
                              ISubscriberService subscriberService,
                              IBlogService blogService,
                              IServiceService serviceService,
                              IBarberService barberService,
                              IFeatureService featureService,
                              IPricingCategoryService pricingCategoryService,
                              IEmployeeService employeeService,
                              IAppointmentService appointmentService,
                              ICommentService commentService,
                              IBarberPricingService barberPricingService,
                              IPositionService positionService,
                              ISettingService settingService,
                              IColleagueService colleagueService)
        {
            _sliderService = sliderService;
            _aboutService = aboutService;
            _historyService = historyService;
            _accessor = accessor;
            _userManager = userManager;
            _subscriberService = subscriberService;
            _blogService = blogService;
            _serviceService = serviceService;
            _barberService = barberService;
            _featureService = featureService;
            _priceCategoryService = pricingCategoryService;
            _employeeService = employeeService;
            _appointmentService = appointmentService;
            _commentService = commentService;
            _barberPricingService = barberPricingService;
            _positionService = positionService;
            _settingService = settingService;
            _colleagueService = colleagueService;
        }
        
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();
            var histories = await _historyService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var services = await _serviceService.GetAll();
            var barberServices = await _barberService.GetAll();
            var features = await _featureService.GetAll();
            var pricingCategoryServices = await _priceCategoryService.GetAll();
            var employeeServices = await _employeeService.GetAll();
            var appointments = await _appointmentService.GetAll();
            var comments = await _commentService.GetAll();
            var barberPricings = await _barberPricingService.GetAll();
            var positions = await _positionService.GetAll();
            //var settings = await _settingService.GetAll();
            var colleagues = await _colleagueService.GetAll();

            HomeVM model = new()
            {
                Sliders = sliders,
                About = abouts,
                History = histories.FirstOrDefault(),
                Blogs = blogs,
                Services = services,
                BarberServices = barberServices,
                Features = features,
                PricingCategories = pricingCategoryServices,
                Employees = employeeServices,
                Appointment = appointments.FirstOrDefault(),
                Comments = comments,
                BarberPricings = barberPricings,
                Positions = positions,
                //Settings = settings
                Colleagues = colleagues,
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
