using BarberProject.Helpers;
using Domain.Models;

namespace BarberProject.ViewModels
{
    public class BlogPageVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Domain.Models.Service> Services { get; set; }
        public Paginate<Blog> Pagination { get; set; }
    }
}
