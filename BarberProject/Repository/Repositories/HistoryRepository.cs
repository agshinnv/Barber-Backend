using Domain.Models;
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
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(AppDbContext context) : base(context) { }

        public async Task Edit(int id, History history)
        {
            var existHistory = await GetById(id);

            existHistory.UpTitle = history.UpTitle;
            existHistory.MainTitle = history.MainTitle;
            existHistory.Description = history.Description;
            existHistory.Image = history.Image;

            await _context.SaveChangesAsync();
        }
    }
}
