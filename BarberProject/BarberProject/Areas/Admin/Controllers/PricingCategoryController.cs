using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Colleagues;
using BarberProject.ViewModels.Positions;
using BarberProject.ViewModels.PricingCategories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PricingCategoryController : Controller
    {
        private readonly IPricingCategoryService _pricingCategoryService;
        private readonly IWebHostEnvironment _env;

        public PricingCategoryController(IPricingCategoryService pricingCategoryService,
                                         IWebHostEnvironment env)
        {
            _pricingCategoryService = pricingCategoryService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _pricingCategoryService.GetAll();

            List<PricingCategoryVM> model = datas.Select(m => new PricingCategoryVM { Id = m.Id, Name = m.Name, Image = m.Image }).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PricingCategoryCreateVM request)
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

            await _pricingCategoryService.Create(new PricingCategory { Name = request.Name, Image = fileName });

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existPricingCategory = await _pricingCategoryService.GetById((int)id);
            if (existPricingCategory is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "images", existPricingCategory.Image);
            existImage.DeleteFileFromLocal();

            await _pricingCategoryService.Delete(existPricingCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existPricingCategory = await _pricingCategoryService.GetById((int)id);
            if (existPricingCategory is null) return NotFound();

            PricingCategoryEditVM model = new()
            {
                ExistImage = existPricingCategory.Image,
                Name = existPricingCategory.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PricingCategoryEditVM request)
        {
            if (!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existPricingCategory = await _pricingCategoryService.GetById((int)id);
            if (existPricingCategory is null) return NotFound();


            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existPricingCategory.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 2 Mb");
                    request.ExistImage = existPricingCategory.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existPricingCategory.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);
                await _pricingCategoryService.Edit((int)id, new PricingCategory { Name = request.Name, Image = fileName });
            }
            else
            {
                await _pricingCategoryService.Edit((int)id, new PricingCategory { Name = request.Name ,Image = existPricingCategory.Image });
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
