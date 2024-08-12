﻿using BarberProject.ViewModels;
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

            ServiceDetailVM service = new()
            {
                Title = existService.Title,
                Description = existService.Description,
                IconImage = existService.IconImage,
                ServiceImages = existService.ServiceImages.Select(m => new ServiceImage { Image = m.Image }).ToList(),
            };

            IEnumerable<Domain.Models.Service> services = await _serviceService.GetAll();
            IEnumerable<SubService> subServices = await _subServiceService.GetSubServicesByService(existService.Id);


            //SubServiceVM subServiceData = new()
            //{
            //    Id = existService.Id,
            //    SubServiceName = existService.Title,
            //    SubServicePrice = existService.
            //};

            ServiceDetailPageVM model = new()
            {
                Service = service,
                Services = services.ToList(),
                SubServices = subServices.ToList(),
            };


            return View(model);
        }
    }
}
