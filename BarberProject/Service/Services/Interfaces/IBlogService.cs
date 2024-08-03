using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task Create(Blog service);
        Task EditAsync(int id, Blog service);
        Task DeleteAsync(Blog blog);
        Task DeleteImage(BlogImage image);
        Task ChangeMainImage(Blog blog, int id);
    }
}
