using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IFeatureService _featureService;
        private readonly IAppointmentService _appointmentService;
        private readonly IEmployeeService _employeeService;

        public ServicesController(IServiceService serviceService, 
                                  IFeatureService featureService, 
                                  IAppointmentService appointmentService,
                                  IEmployeeService employeeService)
        {
            _serviceService = serviceService;
            _featureService = featureService;
            _appointmentService = appointmentService;
            _employeeService = employeeService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAll();
            var features = await _featureService.GetAll();
            var appointments = await _appointmentService.GetAll();



            var serviceLists = await _serviceService.GetAll();

            ViewBag.serviceList = new SelectList(serviceLists, "Id", "Title");



            var employeeLists = await _employeeService.GetAll();

            ViewBag.employeeList = new SelectList(employeeLists, "Id", "BarberName");



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
