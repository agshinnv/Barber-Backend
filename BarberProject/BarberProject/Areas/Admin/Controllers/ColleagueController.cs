using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Colleagues;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ColleagueController : Controller
    {
        private readonly IColleagueService _colleagueService;
        private readonly IWebHostEnvironment _env;

        public ColleagueController(IColleagueService colleagueService,
                                   IWebHostEnvironment env)
        {
            _colleagueService = colleagueService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _colleagueService.GetAll();

            List<ColleagueVM> model = datas.Select(m=> new ColleagueVM { Id = m.Id, Image = m.Image }).ToList();

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
        public async Task<IActionResult> Create(ColleagueCreateVM request)
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

            await _colleagueService.Create(new Colleague { Image = fileName });

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existColleague = await _colleagueService.GetById((int)id);
            if (existColleague is null) return NotFound();

            ColleagueDetailVM model = new()
            {
                Image = existColleague.Image,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existColleague = await _colleagueService.GetById((int)id);
            if (existColleague is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "images", existColleague.Image);
            existImage.DeleteFileFromLocal();

            await _colleagueService.Delete(existColleague);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existColleague = await _colleagueService.GetById((int)id);
            if (existColleague is null) return NotFound();

            ColleagueEditVM model = new()
            {
                ExistImage = existColleague.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ColleagueEditVM request)
        {
            if (!ModelState.IsValid) return View();

            if (id is null) return BadRequest();
            var existColleague = await _colleagueService.GetById((int)id);
            if (existColleague is null) return NotFound();


            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existColleague.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 2 Mb");
                    request.ExistImage = existColleague.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existColleague.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);
                await _colleagueService.Edit((int)id, new Colleague { Image = fileName });
            }
            else
            {
                await _colleagueService.Edit((int)id, new Colleague { Image = existColleague.Image });
            }


            return RedirectToAction(nameof(Index));
        }
    }
}


