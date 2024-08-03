using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Blogs
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public List<IFormFile> BlogImages { get; set; }
        public int ServiceId { get; set; }
    }
}
