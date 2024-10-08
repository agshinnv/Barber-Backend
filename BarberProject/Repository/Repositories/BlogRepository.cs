﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Blogs.Include(m=>m.Service)
                                       .Include(m=>m.BlogImages).ToListAsync();
        }
        
        public async Task<Blog> GetById(int id)
        {
            return await _context.Blogs.Where(m=>m.Id == id)
                                       .Include(m=>m.Service)
                                       .Include(m=>m.BlogImages).FirstOrDefaultAsync();
        }

        public async Task DeleteImage(BlogImage image)
        {
            _context.BlogImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeMainImage(Blog blog, int imageId)
        {
            var images = blog.BlogImages.Where(m => m.IsMain == true);
            foreach (var image in images)
            {
                image.IsMain = false;
            }

            blog.BlogImages.FirstOrDefault(m => m.Id == imageId).IsMain = true;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Blog>> GetAllWithServices()
        {
            return await _context.Blogs.Include(m => m.Service)
                                       .Include(m => m.BlogImages).ToListAsync();
        }

        public async Task<List<Blog>> GetAllPaginatedDatas(int page, int take = 2)
        {
            return await _context.Blogs.Include(m => m.BlogImages)
                                          .Include(m => m.Service)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Blogs.CountAsync();
        }
    }
}
