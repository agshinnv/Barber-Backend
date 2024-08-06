using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetCommentByBlog(int id)
        {
            return await _context.Comments.Where(m => m.BlogId == id).Include(m => m.User).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllWithIncludes()
        {
            return await _context.Comments.Include(m => m.User).Include(m=>m.Blogs).ToListAsync();
        }

    }
}
