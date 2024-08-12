using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class PricingController : Controller
    {
        private readonly IBarberPricingService _barberPricingService;
        private readonly IPricingCategoryService _priceCategoryService;
        private readonly IAppointmentService _appointmentService;

        public PricingController(IBarberPricingService barberPricingService,
                                 IPricingCategoryService priceCategoryService,
                                 IAppointmentService appointmentService)
        {
            _barberPricingService = barberPricingService;
            _priceCategoryService = priceCategoryService;
            _appointmentService = appointmentService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var barberPricings = await _barberPricingService.GetAll();
            var pricingCategoryServices = await _priceCategoryService.GetAll();
            var appointments = await _appointmentService.GetAll();


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
