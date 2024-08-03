using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        Task<Blog> GetById(int id);
        Task<IEnumerable<Blog>> GetAll();
        Task DeleteImage(BlogImage image);
        Task ChangeMainImage(Blog blog, int id);
    }
}
