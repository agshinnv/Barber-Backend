using BarberProject.Helpers;
using BarberProject.ViewModels;
using BarberProject.ViewModels.Blogs;
using BarberProject.ViewModels.Comments;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IServiceService _serviceService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;


        public BlogController(IBlogService blogService,
                              IServiceService serviceService,
                              UserManager<AppUser> userManager,
                              ICommentService commentService)
        {
            _blogService = blogService;
            _serviceService = serviceService;
            _userManager = userManager;
            _commentService = commentService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            //List<BlogVM> model = blogs.Select(m=> new BlogVM { Id = m.Id, Title = m.BlogTitle, Service = m.Service.Title, CreateDate = m.CreatedDate.ToString("dd/MM/yyyy"), Content = m.Content, Image = m.BlogImages.FirstOrDefault(m=>m.IsMain).Image}).ToList();

            var paginatedDatas = await _blogService.GetAllPaginatedDatas(page);
            int blogCount = await _blogService.GetCount();
            int pageCount = _blogService.GetPageCount(blogCount, 2);

            Paginate<Blog> pagination = new(paginatedDatas, pageCount, page);

            var blogs = await _blogService.GetAllWithServices();
            var services = await _serviceService.GetAll();


            BlogPageVM model = new()
            {
                Blogs = blogs.ToList(),
                Services = services.ToList(),
                Pagination = pagination
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string userId, int blogId, string comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Problem();
            }

            await _commentService.Create(new Comment { BlogId = blogId, UserId = userId, CommentText = comment });
            return Ok();
        }
    }
}
