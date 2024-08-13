using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.SliderImages;
using BarberProject.ViewModels.Sliders;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class SliderImageController : Controller
	{
		private readonly ISliderImageService _sliderImageService;
		private readonly IWebHostEnvironment _env;
		public SliderImageController(ISliderImageService sliderImageService,
									 IWebHostEnvironment env)
		{
			_sliderImageService = sliderImageService;
			_env = env;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var datas = await _sliderImageService.GetAll();

			List<SliderImageVM> model = datas.Select(m => new SliderImageVM { Id = m.Id, Image = m.Image }).ToList();

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
		public async Task<IActionResult> Create(SliderImageCreateVM request)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}

			foreach (var item in request.SliderImages)
			{
				if (!item.CheckFileType("image/"))
				{
					ModelState.AddModelError("SliderImages", "File type must be image");
					return View();
				}

				if (!item.CheckFileSize(2))
				{
					ModelState.AddModelError("SliderImages", "Image size must be less than 2");
					return View();
				}
			}


			foreach (var item in request.SliderImages)
			{
				string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

				string path = Path.Combine(_env.WebRootPath, "images", fileName);

				await item.SaveFileToLocalAsync(path);

				await _sliderImageService.Create(new SliderImage { Image = fileName });
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();

			SliderImage sliderImage = await _sliderImageService.GetById((int)id);

			if (sliderImage is null) return NotFound();

			string path = Path.Combine(_env.WebRootPath, "images", sliderImage.Image);
			path.DeleteFileFromLocal();

			await _sliderImageService.Delete(sliderImage);

			return RedirectToAction(nameof(Index));

        }



	}
}
