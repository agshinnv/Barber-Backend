using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Abouts;
using BarberProject.ViewModels.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IWebHostEnvironment _env;

        public ServiceController(IServiceService serviceService,
                                 IWebHostEnvironment env)
        {
            _serviceService = serviceService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _serviceService.GetAll();

            List<ServiceVM> model = datas.Select(m=> new ServiceVM { Id = m.Id, Title = m.Title, IconImage = m.IconImage }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            bool isExist = await _serviceService.ServiceIsExist(request.Title.Trim());

            if (isExist)
            {
                ModelState.AddModelError("Title", "This service has already been created");
                return View();
            }

            foreach (var item in request.ServiceImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("ServiceImages", "File type must be image format");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("ServiceImages", "File size must be less than 2 Mb");
                    return View();
                }


            }

            List<ServiceImage> images = new();

            foreach (var item in request.ServiceImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ServiceImage
                {
                    Image = fileName
                });
            }

            if (!request.IconImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("IconImage", "File type must be image");
                return View();
            }

            if (!request.IconImage.CheckFileSize(2))
            {
                ModelState.AddModelError("IconImage", "Image size must be less than 2 Mb");
                return View();
            }

            string iconFileName = Guid.NewGuid().ToString() + "-" + request.IconImage.FileName;
            string iconPath = Path.Combine(_env.WebRootPath, "images", iconFileName);
            await request.IconImage.SaveFileToLocalAsync(iconPath);


            Domain.Models.Service service = new()
            {
                Title = request.Title,
                Description = request.Description,
                IconImage = iconFileName,
                ServiceImages = images.Select(m => new ServiceImage { Image = m.Image }).ToList(),
            };

            await _serviceService.Create(service);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var service = await _serviceService.GetById((int)id);

            if (service is null) return NotFound();

            ServiceDetailVM model = new()
            {
                Title = service.Title,
                Description = service.Description,
                IconImage = service.IconImage,
                ServiceImages = service.ServiceImages.Select(m => new ServiceImage { Image = m.Image }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existService = await _serviceService.GetById((int)id);

            if (existService is null) return NotFound();

            foreach (var item in existService.ServiceImages)
            {
                var path = Path.Combine(_env.WebRootPath, "images", item.Image);
                path.DeleteFileFromLocal();
            }

            var iconPath = Path.Combine(_env.WebRootPath, "images", existService.IconImage);
            iconPath.DeleteFileFromLocal();


            await _serviceService.Delete(existService);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? serviceId)
        {
            if (id is null) return BadRequest();

            Domain.Models.Service service = await _serviceService.GetById((int)serviceId);

            if (service is null) return NotFound();

            var existImage = service.ServiceImages.FirstOrDefault(m => m.Id == id);

            string path = Path.Combine(_env.WebRootPath, "images", existImage.Image);
            path.DeleteFileFromLocal();

            await _serviceService.DeleteImage(existImage);

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();

            var existService = await _serviceService.GetById((int)id);

            if (existService is null) return NotFound();

            ServiceEditVM model = new()
            {
                Title = existService.Title,
                Description = existService.Description,
                ExistIconImage = existService.IconImage,
                ExistServiceImages = existService.ServiceImages.Select(m => new ServiceEditImageVM { Id = m.Id, Name = m.Image, ServiceId = m.ServiceId }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceEditVM request)
        {
            var service = await _serviceService.GetAll();

            if (id is null) return BadRequest();

            var existService = await _serviceService.GetById((int)id);
            request.ExistIconImage = existService.IconImage;

            if (existService is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistServiceImages = existService.ServiceImages.Select(m => new ServiceEditImageVM { Id = m.Id, Name = m.Image, ServiceId = m.ServiceId }).ToList();
                request.ExistIconImage = existService.IconImage;
                return View(request);
            }

            List<ServiceImage> images = existService.ServiceImages.ToList();

            if (request.NewServiceImages is not null)
            {
                foreach (var item in request.NewServiceImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewServiceImages", "File type must be image");
                        request.ExistServiceImages = existService.ServiceImages.Select(m => new ServiceEditImageVM { Id = m.Id, Name = m.Image, ServiceId = m.ServiceId }).ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(2))
                    {
                        ModelState.AddModelError("NewServiceImages", "Image size must be less than 2 Mb");
                        request.ExistServiceImages = existService.ServiceImages.Select(m => new ServiceEditImageVM { Id = m.Id, Name = m.Image, ServiceId = m.ServiceId }).ToList();
                        return View(request);
                    }


                }

                foreach (var item in request.NewServiceImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "images", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new ServiceImage
                    {
                        Image = fileName
                    });
                }

                
            }

            if (request.NewIconImage is not null)
            {
                if (!request.NewIconImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewIconImage", "File type must be image");
                    request.ExistIconImage = existService.IconImage;
                    return View(request);
                }

                if (!request.NewIconImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewIconImage", "Image size must be less than 2 Mb");
                    request.ExistIconImage = existService.IconImage;
                    return View(request);
                }

                string oldIconImagePath = Path.Combine(_env.WebRootPath, "images", existService.IconImage);
                oldIconImagePath.DeleteFileFromLocal();

                string iconFileName = Guid.NewGuid().ToString() + "-" + request.NewIconImage.FileName;
                string iconImagePath = Path.Combine(_env.WebRootPath, "images", iconFileName);
                await request.NewIconImage.SaveFileToLocalAsync(iconImagePath);

                request.ExistIconImage = iconFileName;

            }


            await _serviceService.Edit((int)id, new Domain.Models.Service
            {
                ServiceImages = images,
                Title = request.Title,
                Description = request.Description,
                IconImage = request.ExistIconImage
            });



            return RedirectToAction(nameof(Index));
        }


    }
}
