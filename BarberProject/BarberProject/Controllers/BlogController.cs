using BarberProject.ViewModels.Blogs;
using BarberProject.ViewModels.Comments;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existBlog = await _blogService.GetByIdAsync((int)id);

            if (existBlog is null) return NotFound();

            BlogDetailVM blog = new()
            {
                Id = existBlog.Id,
                Title = existBlog.BlogTitle,
                Description = existBlog.Description,
                Content = existBlog.Content,
                CreateDate = existBlog.CreatedDate,
                Service = existBlog.Service.Title,
                BlogImages = existBlog.BlogImages.Select(m => new BlogImageVM { Image = m.Image, IsMain = m.IsMain }).ToList(),
            };

            IEnumerable<Domain.Models.Service> services = await _serviceService.GetAll();
            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();
            IEnumerable<Comment> comments = await _commentService.GetCommentsByBlog(existBlog.Id);

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            CommentVM commentData = new()
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserName = user.FullName,
                ServiceId = existBlog.Id,
            };

            BlogDetailPageVM model = new()
            {
                Blog = blog,
                Blogs = blogs,
                Services = services,
                CommentData = commentData,
                BlogComments = comments
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
