using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<History>> GetAllAsync();
        Task<History> GetById(int id);
        Task Delete(History history);
        Task Create(History history);
        Task Edit(int id, History history);

    }
}
