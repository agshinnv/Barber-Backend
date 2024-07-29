using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Abouts;
using BarberProject.ViewModels.Sliders;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Mozilla;
using Service.Services.Interfaces;
using System.Collections.Generic;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;

        public SliderController(ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _sliderService.GetAllAsync();

            List<SliderVM> model = datas.Select(m => new SliderVM { Id = m.Id, Description = m.SliderDescription, Title = m.SliderTitle }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if(!ModelState.IsValid) return View();

            foreach (var item in request.SliderImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("SliderImage", "File type must be image format");
                    return View();
                }

                if (!item.CheckFileSize(3))
                {
                    ModelState.AddModelError("SliderImage", "File size must be less than 3 Mb");
                    return View();
                }
            }

            List<SliderImage> images = new();

            foreach (var item in request.SliderImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new SliderImage
                {
                    Image = fileName
                });
            }


            Slider slider = new()
            {
                SliderTitle = request.SliderTitle,
                SliderDescription = request.SliderDesc,
                SliderImages = images.Select(m => new SliderImage { Image = m.Image }).ToList(),
            };

            await _sliderService.Create(slider);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync((int)id);

            if(slider is null) return NotFound();

            SliderDetailVM model = new()
            {
                SliderTitle = slider.SliderTitle,
                SliderDesc = slider.SliderDescription,
                SliderImages = slider.SliderImages.Select(m => new SliderImage { Image = m.Image }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetByIdAsync((int)id);

            if (existSlider is null) return NotFound();

            foreach (var item in existSlider.SliderImages)
            {
                var path = Path.Combine(_env.WebRootPath, "images", item.Image);
                path.DeleteFileFromLocal();
            }

          

            await _sliderService.DeleteAsync(existSlider);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? sliderId)
        {
            if (id is null) return BadRequest();

            Slider slider = await _sliderService.GetByIdAsync((int)sliderId);

            if (slider is null) return NotFound();

            var existImage = slider.SliderImages.FirstOrDefault(m => m.Id == id);

            string path = Path.Combine(_env.WebRootPath, "images", existImage.Image);
            path.DeleteFileFromLocal();

            await _sliderService.DeleteImage(existImage);

            return Ok();

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            
            var existSlider = await _sliderService.GetByIdAsync((int) id);

            if (existSlider is null) return NotFound();

            SliderEditVM model = new()
            {
                SliderTitle = existSlider.SliderTitle,
                SliderDescription = existSlider.SliderDescription,
                ExistImages = existSlider.SliderImages.Select(m=> new SliderEditImageVM { Id = m.Id, Name = m.Image, SliderId = m.SliderId }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            var slider = await _sliderService.GetAllAsync();

            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetByIdAsync((int)id);

            if (existSlider is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistImages = existSlider.SliderImages.Select(m => new SliderEditImageVM { Id = m.Id, Name = m.Image, SliderId = m.SliderId }).ToList();
                return View(request);
            }

            List<SliderImage> images = existSlider.SliderImages.ToList();

            if (request.NewSliderImages is not null)
            {
                foreach (var item in request.NewSliderImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewSliderImages", "File type must be image");
                        request.ExistImages = existSlider.SliderImages.Select(m => new SliderEditImageVM { Id = m.Id, Name = m.Image, SliderId = m.SliderId }).ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(1))
                    {
                        ModelState.AddModelError("NewSliderImages", "Image size must be less than 1 Mb");
                        request.ExistImages = existSlider.SliderImages.Select(m => new SliderEditImageVM { Id = m.Id, Name = m.Image, SliderId = m.SliderId }).ToList();
                        return View(request);
                    }
                }

                foreach (var item in request.NewSliderImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "images", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new SliderImage
                    {
                        Image = fileName
                    });
                }
            }


            Slider modelSlider = new()
            {
                SliderTitle = request.SliderTitle,
                SliderDescription = request.SliderDescription,
                SliderImages = images
            };

            await _sliderService.EditAsync((int)id, modelSlider);

            return RedirectToAction(nameof(Index));
        }




    }
}
