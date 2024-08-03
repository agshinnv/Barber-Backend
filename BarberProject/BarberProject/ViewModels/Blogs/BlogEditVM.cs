using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Blogs
{
    public class BlogEditVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        public int ServiceId { get; set; }
        public List<BlogEditImageVM> ExistBlogImages { get; set; }
        public List<IFormFile> NewBlogImages { get; set; }
    }
}
