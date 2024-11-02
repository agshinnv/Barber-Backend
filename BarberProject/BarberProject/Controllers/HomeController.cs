using BarberProject.ViewModels;
using BarberProject.ViewModels.Reservation;
using BarberProject.ViewModels.Users;
using Domain.Helpers.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderImageService _sliderImageService;
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
        private readonly IWorkTimeService _workTimeService;
        private readonly IAccountService _accountService;
        private readonly IReservationService _reservationService;

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
                              IColleagueService colleagueService,
                              IWorkTimeService workTimeService,
                              ISliderImageService sliderImageService,
                              IAccountService accountService,
                              IReservationService reservationService)
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
            _workTimeService = workTimeService;
            _sliderImageService = sliderImageService;
            _accountService = accountService;
            _reservationService = reservationService;
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
            var settings = await _settingService.GetAll();
            var colleagues = await _colleagueService.GetAll();
            var workTimes = await _workTimeService.GetAll();
            var sliderImages = await _sliderImageService.GetAll();
            var accounts = await _accountService.GetAll();
            var reservDates = await _reservationService.GetAll();

            Dictionary<string, string> values = new();

            foreach (KeyValuePair<int, Dictionary<string, string>> item in settings)
            {
                values.Add(item.Value["Key"], item.Value["Value"]);
            }

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            UserVM userData = new()
            {
                FullName = user.FullName,
            };

            var serviceLists = await _serviceService.GetAll();

            ViewBag.serviceList = new SelectList(serviceLists, "Id", "Title");



            var employeeLists = await _employeeService.GetAll();

            ViewBag.employeeList = new SelectList(employeeLists, "Id", "BarberName");

            
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
                Settings = values,
                Colleagues = colleagues,
                WorkTimes = workTimes,
                SliderImages = sliderImages,
                Users = accounts,
                UserData = userData,
                ReservDates = reservDates.Where(m => m.OrderStatus == OrderStatus.Accepted).Select(m => new ReservDatesVM { Date = m.Date.ToString("yyyy,MM,dd"), Time = m.Time.ToString("hh:mm tt") }).ToList()
            };

            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string subscriberEmail)
        {
            await _subscriberService.Create(new Subscriber { SubscriberEmail = subscriberEmail });
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> AddReservation(OrderVM request)
        {
            var employee = await _employeeService.GetById(request.EmployeeId);
            var date = Convert.ToDateTime(request.Date);
            var time = request.Time;

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (request is not null)
                {

                    Reservation reservation = new()
                    {
                        EmployeeId = request.EmployeeId,
                        ServiceId = request.ServiceId,
                        Date = date,
                        Time = Convert.ToDateTime(time),
                        UserId = user.Id,

                    };

                    await _reservationService.Create(reservation);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            if (employee is null) return NotFound();

            return Ok();
        }

        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 404)
            {
                ViewBag.ErrorMessage = "Sorry We Can't Find That Page!";
            }

            return View("Error");

        }

    }
}
