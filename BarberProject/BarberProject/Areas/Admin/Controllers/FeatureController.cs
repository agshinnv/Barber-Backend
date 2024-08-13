using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Abouts;
using BarberProject.ViewModels.Appointments;
using BarberProject.ViewModels.Features;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly IWebHostEnvironment _env;

        public FeatureController(IFeatureService featureService,
                                 IWebHostEnvironment env)
        {
            _featureService = featureService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _featureService.GetAll();

            List<FeatureVM> model = datas.Select(m=> new FeatureVM { Id = m.Id, ServiceName = m.ServiceName, Description = m.Description }).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeatureCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File type must be image");
                return View();
            }

            if (!request.Image.CheckFileSize(2))
            {
                ModelState.AddModelError("Image", "Image size must be less than 2 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "images", fileName);
            await request.Image.SaveFileToLocalAsync(path);
            
            await _featureService.Create(new Feature { ServiceName = request.ServiceName, Description = request.Description, Image = fileName });

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            FeatureDetailVM model = new()
            {
                ServiceName = existFeature.ServiceName,
                Description = existFeature.Description,
                Image = existFeature.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "images", existFeature.Image);
            existImage.DeleteFileFromLocal();


            await _featureService.Delete(existFeature);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            FeatureEditVM model = new()
            {
                ServiceName = existFeature.ServiceName,
                Description = existFeature.Description,
                ExistImage = existFeature.Image
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, FeatureEditVM request)
        {

            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistImage = existFeature.Image;
                return View(request);
            }

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existFeature.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 2 Mb");
                    request.ExistImage = existFeature.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existFeature.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);
                await _featureService.Edit((int)id, new Feature { ServiceName = request.ServiceName, Description = request.Description, Image = fileName });

            }
            else
            {
                await _featureService.Edit((int)id, new Feature { ServiceName = request.ServiceName, Description = request.Description, Image = existFeature.Image });
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
