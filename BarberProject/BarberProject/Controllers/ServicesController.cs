using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IFeatureService _featureService;
        private readonly IAppointmentService _appointmentService;

        public ServicesController(IServiceService serviceService, 
                                  IFeatureService featureService, 
                                  IAppointmentService appointmentService)
        {
            _serviceService = serviceService;
            _featureService = featureService;
            _appointmentService = appointmentService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAll();
            var features = await _featureService.GetAll();
            var appointments = await _appointmentService.GetAll();

            ServicePageVM model = new()
            {
                Services = services,
                Features = features,
                Appointment = appointments.FirstOrDefault(),
            };


            return View(model);
        }
    }
}
