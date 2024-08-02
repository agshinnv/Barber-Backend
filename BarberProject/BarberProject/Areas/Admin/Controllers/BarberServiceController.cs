using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.BarberServices;
using BarberProject.ViewModels.Colleagues;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BarberServiceController : Controller
    {
        private readonly IBarberService _barberService;
        private readonly IWebHostEnvironment _env;

        public BarberServiceController(IBarberService barberService,
                                       IWebHostEnvironment env)
        {
            _barberService = barberService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _barberService.GetAll();

            List<BarberServiceVM> model = datas.Select(m => new BarberServiceVM { Id = m.Id, ServiceImage = m.ServiceImage,
                                                                                  Price = m.Price, ServiceName = m.ServiceName }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarberServiceCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.ServiceImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("ServiceImage", "File type must be image");
                return View();
            }

            if (!request.ServiceImage.CheckFileSize(2))
            {
                ModelState.AddModelError("ServiceImage", "Image size must be less than 2 Mb");
                return View();
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


            string serviceFileName = Guid.NewGuid().ToString() + "-" + request.ServiceImage.FileName;
            string servicePath = Path.Combine(_env.WebRootPath, "images", serviceFileName);
            await request.ServiceImage.SaveFileToLocalAsync(servicePath);

            string iconFileName = Guid.NewGuid().ToString() + "-" + request.IconImage.FileName;
            string iconPath = Path.Combine(_env.WebRootPath, "images", iconFileName);
            await request.IconImage.SaveFileToLocalAsync(iconPath);

            await _barberService.Create(new Domain.Models.BarberService { ServiceName = request.ServiceName, ServiceDescription = request.ServiceDescription,
                                                                          Price = request.Price, ServiceImage = serviceFileName, IconImage = iconFileName});

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existBarberService = await _barberService.GetById((int)id);
            if (existBarberService is null) return NotFound();
            
            BarberServiceDetailVM model = new()
            {
                ServiceName = existBarberService.ServiceName,
                Description = existBarberService.ServiceDescription,
                Price = existBarberService.Price,
                ServiceImage = existBarberService.ServiceImage,
                IconImage = existBarberService.IconImage,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existBarberService = await _barberService.GetById((int)id);
            if (existBarberService is null) return NotFound();
            
            string existServiceImage = Path.Combine(_env.WebRootPath, "images", existBarberService.ServiceImage);
            existServiceImage.DeleteFileFromLocal();

            string existIconImage = Path.Combine(_env.WebRootPath, "images", existBarberService.IconImage);
            existIconImage.DeleteFileFromLocal();

            await _barberService.Delete(existBarberService);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existBarberService = await _barberService.GetById((int)id);
            if (existBarberService is null) return NotFound();

            BarberServiceEditVM model = new()
            {
                ServiceName = existBarberService.ServiceName,
                ServiceDescription = existBarberService.ServiceDescription,
                Price = existBarberService.Price,
                ExistServiceImage = existBarberService.ServiceImage,
                ExistIconImage = existBarberService.IconImage,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BarberServiceEditVM request)
        {
            if (!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existBarberService = await _barberService.GetById((int)id);
            if (existBarberService is null) return NotFound();
            request.ExistServiceImage = existBarberService.ServiceImage;
            request.ExistIconImage = existBarberService.IconImage;


            if (request.NewServiceImage is not null)
            {
                if (!request.NewServiceImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewServiceImage", "File type must be image");
                    request.ExistServiceImage = existBarberService.ServiceImage;
                    return View(request);
                }

                if (!request.NewServiceImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewServiceImage", "Image size must be less than 2 Mb");
                    request.ExistServiceImage = existBarberService.ServiceImage;
                    return View(request);
                }

                string oldServiceImagePath = Path.Combine(_env.WebRootPath, "images", existBarberService.ServiceImage);
                oldServiceImagePath.DeleteFileFromLocal();

                string serviceFileName = Guid.NewGuid().ToString() + "-" + request.NewServiceImage.FileName;
                string serviceImagePath = Path.Combine(_env.WebRootPath, "images", serviceFileName);
                await request.NewServiceImage.SaveFileToLocalAsync(serviceImagePath);

                request.ExistServiceImage = serviceFileName;

            }

            if (request.NewIconImage is not null)
            {
                if (!request.NewIconImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewIconImage", "File type must be image");
                    request.ExistIconImage = existBarberService.IconImage;
                    return View(request);
                }

                if (!request.NewIconImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewIconImage", "Image size must be less than 2 Mb");
                    request.ExistIconImage = existBarberService.IconImage;
                    return View(request);
                }

                string oldIconImagePath = Path.Combine(_env.WebRootPath, "images", existBarberService.IconImage);
                oldIconImagePath.DeleteFileFromLocal();

                string iconFileName = Guid.NewGuid().ToString() + "-" + request.NewIconImage.FileName;
                string iconImagePath = Path.Combine(_env.WebRootPath, "images", iconFileName);
                await request.NewIconImage.SaveFileToLocalAsync(iconImagePath);

                request.ExistIconImage = iconFileName;
            }


            await _barberService.Edit((int)id, new Domain.Models.BarberService
            {
                ServiceImage = request.ExistServiceImage,
                ServiceName = request.ServiceName,
                ServiceDescription = request.ServiceDescription,
                Price = request.Price,
                IconImage = request.ExistIconImage
            });





            return RedirectToAction(nameof(Index));
        }



    }
}
