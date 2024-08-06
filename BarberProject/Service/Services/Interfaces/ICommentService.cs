using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAll();
        Task Create(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByBlog(int id);
        Task<Comment> GetById(int id);
        Task Delete(Comment comment);
    }
}
