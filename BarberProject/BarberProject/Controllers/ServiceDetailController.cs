using BarberProject.ViewModels;
using BarberProject.ViewModels.Blogs;
using BarberProject.ViewModels.Services;
using BarberProject.ViewModels.SubServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class ServiceDetailController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IBlogService _blogService;
        private readonly ISubServiceService _subServiceService;

        public ServiceDetailController(IServiceService serviceService,
                                       IBlogService blogService,
                                       ISubServiceService subServiceService)
        {
            _serviceService = serviceService;
            _blogService = blogService;
            _subServiceService = subServiceService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existService = await _serviceService.GetById((int)id);

            if (existService is null) return NotFound();


            IEnumerable<Domain.Models.Service> services = await _serviceService.GetAll();


            //SubServiceVM subServiceData = new()
            //{
            //    Id = existService.Id,
            //    SubServiceName = existService.Title,
            //    SubServicePrice = existService.
            //};

            ServiceDetailPageVM model = new()
            {
                Services = services.ToList(),
            };


            return View(model);
        }
    }
}
