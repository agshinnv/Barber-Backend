using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Abouts;
using BarberProject.ViewModels.Sliders;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using System.Reflection;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;

        public AboutController(IAboutService aboutService,
                               IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _aboutService.GetAllAsync();

            AboutVM model = new()
            {
                Id = datas.Id,
                AboutDesc = datas.Description,
                AboutTitle = datas.Title,
                AboutPro1 = datas.Pro1,
                AboutPro2 = datas.Pro2,
                AboutPro3 = datas.Pro3
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            foreach (var item in request.AboutImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("AboutImages", "File type must be image format");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("AboutImages", "File size must be less than 2 Mb");
                    return View();
                }

                if (request.AboutImages.Count > 2)
                {
                    ModelState.AddModelError("AboutImages", "You can upload a maximum of 2 images.");
                    return View();
                }


            }

            List<AboutImage> images = new();

            foreach (var item in request.AboutImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new AboutImage
                {
                    Image = fileName
                });
            }


            About about = new()
            {
                Title = request.AboutTitle,
                Description = request.AboutDesc,
                Pro1 = request.AboutPro1,
                Pro2 = request.AboutPro2,
                Pro3 = request.AboutPro3,
                AboutImages = images.Select(m => new AboutImage { Image = m.Image }).ToList(),
            };

            await _aboutService.Create(about);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var about = await _aboutService.GetByIdAsync((int)id);

            if (about is null) return NotFound();

            AboutDetailVM model = new()
            {
                AboutTitle = about.Title,
                AboutDesc = about.Description,
                AboutPro1 = about.Pro1,
                AboutPro2 = about.Pro2,
                AboutPro3 = about.Pro3,
                AboutImages = about.AboutImages.Select(m => new AboutImage { Image = m.Image }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existAbout = await _aboutService.GetByIdAsync((int)id);

            if (existAbout is null) return NotFound();

            foreach (var item in existAbout.AboutImages)
            {
                var path = Path.Combine(_env.WebRootPath, "images", item.Image);
                path.DeleteFileFromLocal();
            }




            await _aboutService.DeleteAsync(existAbout);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? aboutId)
        {
            if (id is null) return BadRequest();

            About about = await _aboutService.GetByIdAsync((int)aboutId);

            if (about is null) return NotFound();

            var existImage = about.AboutImages.FirstOrDefault(m => m.Id == id);

            string path = Path.Combine(_env.WebRootPath, "images", existImage.Image);
            path.DeleteFileFromLocal();

            await _aboutService.DeleteImage(existImage);

            return Ok();

        }


        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();

            var existAbout = await _aboutService.GetByIdAsync((int)id);

            if (existAbout is null) return NotFound();

            AboutEditVM model = new()
            {
                AboutTitle = existAbout.Title,
                AboutDesc = existAbout.Description,
                AboutPro1 = existAbout.Pro1,
                AboutPro2 = existAbout.Pro2,
                AboutPro3 = existAbout.Pro3,
                ExistImages = existAbout.AboutImages.Select(m => new AboutEditImageVM { Id = m.Id, Name = m.Image, AboutId = m.AboutId }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            var about = await _aboutService.GetAllAsync();

            if (id is null) return BadRequest();

            var existAbout = await _aboutService.GetByIdAsync((int)id);

            if (existAbout is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistImages = existAbout.AboutImages.Select(m => new AboutEditImageVM { Id = m.Id, Name = m.Image, AboutId = m.AboutId }).ToList();
                return View(request);
            }

            List<AboutImage> images = existAbout.AboutImages.ToList();

            if (request.NewAboutImages is not null)
            {
                foreach (var item in request.NewAboutImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewAboutImages", "File type must be image");
                        request.ExistImages = existAbout.AboutImages.Select(m => new AboutEditImageVM { Id = m.Id, Name = m.Image, AboutId = m.AboutId }).ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(1))
                    {
                        ModelState.AddModelError("NewAboutImages", "Image size must be less than 1 Mb");
                        request.ExistImages = existAbout.AboutImages.Select(m => new AboutEditImageVM { Id = m.Id, Name = m.Image, AboutId = m.AboutId }).ToList();
                        return View(request);
                    }


                }

                foreach (var item in request.NewAboutImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "images", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new AboutImage
                    {
                        Image = fileName
                    });
                }
            }


            About modelAbout = new()
            {
                Title = request.AboutTitle,
                Description = request.AboutDesc,
                Pro1 = request.AboutPro1,
                Pro2 = request.AboutPro2,
                Pro3 = request.AboutPro3,
                AboutImages = images
            };

            await _aboutService.EditAsync((int)id, modelAbout);

            return RedirectToAction(nameof(Index));
        }


    }
}
