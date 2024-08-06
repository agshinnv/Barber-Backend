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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task Create(Comment comment)
        {
            await _commentRepository.Create(comment);
        }

        public async Task Delete(Comment comment)
        {
            await _commentRepository.Delete(comment);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _commentRepository.GetAllWithIncludes();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _commentRepository.GetById(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBlog(int id)
        {
            return await _commentRepository.GetCommentByBlog(id);
        }
    }
}
