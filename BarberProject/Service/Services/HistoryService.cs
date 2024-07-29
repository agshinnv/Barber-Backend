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
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task Create(History history)
        {
            await _historyRepository.Create(history);
        }

        public async Task Delete(History history)
        {
            await _historyRepository.Delete(history);
        }

        public async Task Edit(int id, History history)
        {
            await _historyRepository.Edit(id, history);
        }

        public Task<IEnumerable<History>> GetAllAsync()
        {
            return _historyRepository.GetAll();
        }

        public async Task<History> GetById(int id)
        {
            return await _historyRepository.GetById(id);
        }
    }
}
