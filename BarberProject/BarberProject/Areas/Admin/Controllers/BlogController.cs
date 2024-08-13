using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Abouts;
using BarberProject.ViewModels.Blogs;
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
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IServiceService _serviceService;

        public BlogController(IBlogService blogService,
                              IWebHostEnvironment env,
                              IServiceService serviceService)
        {
            _blogService = blogService;
            _env = env;
            _serviceService = serviceService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _blogService.GetAllAsync();

            List<BlogVM> model = datas.Select(m=> new BlogVM { Id = m.Id, Title = m.BlogTitle, CreateDate = m.CreatedDate.ToString("dd/MM/yyyy")}).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (!ModelState.IsValid)
            {
                return View();
            }



            foreach (var item in request.BlogImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("BlogImages", "File type must be image format");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("BlogImages", "File size must be less than 2 Mb");
                    return View();
                }


            }

            List<BlogImage> images = new();

            foreach (var item in request.BlogImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new BlogImage
                {
                    Image = fileName
                });
            }

            images.FirstOrDefault().IsMain = true;

            Blog blog = new()
            {
                BlogTitle = request.Title,
                Description = request.Description,
                Content = request.Content,
                ServiceId = request.ServiceId,
                BlogImages = images.Select(m => new BlogImage { Image = m.Image, IsMain = m.IsMain }).ToList(),
            };

            await _blogService.Create(blog);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            BlogDetailVM model = new()
            {
                Title = blog.BlogTitle,
                Description = blog.Description,
                Content = blog.Content,
                CreateDate = blog.CreatedDate,
                Service = blog.Service.Title,
                BlogImages = blog.BlogImages.Select(m => new BlogImageVM { Image = m.Image, IsMain = m.IsMain }).ToList(),
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existBlog = await _blogService.GetByIdAsync((int)id);
            if (existBlog is null) return NotFound();

            foreach (var item in existBlog.BlogImages)
            {
                var path = Path.Combine(_env.WebRootPath, "images", item.Image);
                path.DeleteFileFromLocal();
            }

            await _blogService.DeleteAsync(existBlog);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (id is null) return BadRequest();
            var blog = await _blogService.GetByIdAsync((int)id);
            if (blog is null) return NotFound();

            BlogEditVM model = new()
            {
                Title = blog.BlogTitle,
                Description = blog.Description,
                Content = blog.Content,
                ExistBlogImages = blog.BlogImages.Select(m => new BlogEditImageVM { Id = m.Id, Name = m.Image, IsMain = m.IsMain, BlogId = m.BlogId }).ToList(),
                ServiceId = blog.ServiceId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? blogId)
        {
            if (id is null) return BadRequest();

            var blog = await _blogService.GetByIdAsync((int)blogId);

            if (blog is null) return NotFound();


            var existImage = blog.BlogImages.FirstOrDefault(m => m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "images", existImage.Image);
            path.DeleteFileFromLocal();

            await _blogService.DeleteImage(existImage);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id, int? blogId)
        {
            if (id is null || blogId is null) return BadRequest();

            var blog = await _blogService.GetByIdAsync((int)blogId);

            if (blog is null) NotFound();

            await _blogService.ChangeMainImage(blog, (int)id);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            var services = await _serviceService.GetAll();

            ViewBag.services = new SelectList(services, "Id", "Title");

            if (id is null) return BadRequest();

            var existBlog = await _blogService.GetByIdAsync((int)id);

            if (existBlog is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistBlogImages = existBlog.BlogImages.Select(m => new BlogEditImageVM { Id = m.Id, Name = m.Image, BlogId = m.BlogId, IsMain = m.IsMain }).ToList();
                return View(request);
            }

            List<BlogImage> images = existBlog.BlogImages.ToList();

            if (request.NewBlogImages is not null)
            {
                foreach (var item in request.NewBlogImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewBlogImages", "File type must be image");
                        request.ExistBlogImages = existBlog.BlogImages.Select(m => new BlogEditImageVM { Id = m.Id, Name = m.Image, IsMain = m.IsMain, BlogId = m.BlogId }).ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(2))
                    {
                        ModelState.AddModelError("NewBlogImages", "Image size must be less than 2");
                        request.ExistBlogImages = existBlog.BlogImages.Select(m => new BlogEditImageVM { Id = m.Id, Name = m.Image, IsMain = m.IsMain, BlogId = m.BlogId }).ToList();
                        return View(request);
                    }
                }

                foreach (var item in request.NewBlogImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "images", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new BlogImage
                    {
                        Image = fileName
                    });
                }
            }


            Blog blog = new()
            {
                BlogTitle = request.Title,
                Description = request.Description,
                Content = request.Content,
                ServiceId = request.ServiceId,
                BlogImages = images,
            };

            await _blogService.EditAsync((int)id, blog);

            return RedirectToAction(nameof(Index));
        }


    }
}
