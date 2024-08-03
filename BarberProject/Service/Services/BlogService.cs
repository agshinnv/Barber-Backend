using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task Create(Blog service)
        {
            await _blogRepository.Create(service);
        }

        public async Task DeleteAsync(Blog blog)
        {
            await _blogRepository.Delete(blog);
        }

        public async Task DeleteImage(BlogImage image)
        {
            await _blogRepository.DeleteImage(image);
        }
        
        public async Task EditAsync(int id, Blog service)
        {
            var existBlog = await _blogRepository.GetById(id);

            existBlog.BlogTitle = service.BlogTitle;
            existBlog.Description = service.Description;
            existBlog.Content = service.Content;
            existBlog.BlogImages = service.BlogImages;
            existBlog.ServiceId = service.ServiceId;

            await _blogRepository.Edit(existBlog);

        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAll();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogRepository.GetById(id);
        }

        public async Task ChangeMainImage(Blog blog, int id)
        {
            await _blogRepository.ChangeMainImage(blog, id);
        }
    }
}
