using BarberProject.ViewModels;
using BarberProject.ViewModels.Abouts;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Sig;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IServiceService _serviceService;
        private readonly IHistoryService _historyService;
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;

        public AboutController(IAboutService aboutService,
                               IServiceService serviceService,
                               IHistoryService historyService,
                               IEmployeeService employeeService,
                               IPositionService positionService)
        {
            _aboutService = aboutService;
            _serviceService = serviceService;
            _historyService = historyService;
            _employeeService = employeeService;
            _positionService = positionService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            var services = await _serviceService.GetAll();
            var histories = await _historyService.GetAllAsync();
            var employeeServices = await _employeeService.GetAll();
            var positions = await _positionService.GetAll();

            AboutPageVM model = new()
            {
                About = abouts,
                History = histories.FirstOrDefault(),
                Services = services,
                Employees = employeeServices,
                Positions = positions,
            };


            return View(model);
        }
    }
}
