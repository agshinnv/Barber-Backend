using BarberProject.ViewModels.Comments;
using Domain.Models;

namespace BarberProject.ViewModels.Blogs
{
    public class BlogDetailPageVM
    {
        public BlogDetailVM Blog { get; set; }
        public List<Domain.Models.Service> Services { get; set; }
        public List<Blog> Blogs { get; set; }
        public CommentVM CommentData { get; set; }
        public IEnumerable<Comment> BlogComments { get; set; }
    }
}
