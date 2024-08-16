using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class PricingController : Controller
    {
        private readonly IBarberPricingService _barberPricingService;
        private readonly IPricingCategoryService _priceCategoryService;
        private readonly IAppointmentService _appointmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceService _serviceService;

        public PricingController(IBarberPricingService barberPricingService,
                                 IPricingCategoryService priceCategoryService,
                                 IAppointmentService appointmentService,
                                 IEmployeeService employeeService,
                                 IServiceService serviceService)
        {
            _barberPricingService = barberPricingService;
            _priceCategoryService = priceCategoryService;
            _appointmentService = appointmentService;
            _employeeService = employeeService;
            _serviceService = serviceService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var barberPricings = await _barberPricingService.GetAll();
            var pricingCategoryServices = await _priceCategoryService.GetAll();
            var appointments = await _appointmentService.GetAll();
            var services = await _serviceService.GetAll();


            var serviceLists = await _serviceService.GetAll();

            ViewBag.serviceList = new SelectList(serviceLists, "Id", "Title");



            var employeeLists = await _employeeService.GetAll();

            ViewBag.employeeList = new SelectList(employeeLists, "Id", "BarberName");



            PricingPageVM model = new()
            {
                BarberPricings = barberPricings,
                PricingCategories = pricingCategoryServices,
                Appointment = appointments.FirstOrDefault(),
            };

            return View(model);
        }
    }
}
