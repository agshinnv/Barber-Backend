using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Histories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System.Reflection;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IWebHostEnvironment _env;

        public HistoryController(IHistoryService historyService,
                                 IWebHostEnvironment env)
        {
            _historyService = historyService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _historyService.GetAllAsync();

            List<HistoryVM> model = datas.Select(m=> new HistoryVM { Id = m.Id, UpTitle = m.UpTitle}).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistoryCreateVM request)
        {
            if(!ModelState.IsValid) return View();

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
            string path = Path.Combine(_env.WebRootPath,"images", fileName);
            await request.Image.SaveFileToLocalAsync(path);

            await _historyService.Create(new History { UpTitle = request.UpTitle, MainTitle = request.MainTitle, Description = request.Description, Image = fileName });
            
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existHistory = await _historyService.GetById((int)id);
            if (existHistory is null) return NotFound();

            HistoryDetailVM model = new()
            {
                UpTitle = existHistory.UpTitle,
                MainTitle = existHistory.MainTitle,
                Description = existHistory.Description,
                Image = existHistory.Image,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existHistory = await _historyService.GetById((int)id);
            if (existHistory is null) return NotFound();

            await _historyService.Delete(existHistory);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existHistory = await _historyService.GetById((int)id);
            if (existHistory is null) return NotFound();

            HistoryEditVM model = new()
            {
                UpTitle = existHistory.UpTitle,
                MainTitle = existHistory.MainTitle,
                Description = existHistory.Description,
                ExistImage = existHistory.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, HistoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();
            var existHistory = await _historyService.GetById((int)id);
            if (existHistory is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existHistory.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(1))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 1 Mb");
                    request.ExistImage = existHistory.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existHistory.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);

                await _historyService.Edit((int)id, new History { UpTitle = request.UpTitle, MainTitle = request.MainTitle, Description = request.Description, Image = fileName });
            }
            else
            {
                await _historyService.Edit((int)id, new History { UpTitle = request.UpTitle, MainTitle = request.MainTitle, Description = request.Description, Image = existHistory.Image });
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
