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
    public class ColleagueService : IColleagueService
    {
        private readonly IColleagueRepository _colleagueRepository;

        public ColleagueService(IColleagueRepository colleagueRepository)
        {
            _colleagueRepository = colleagueRepository;
        }

        public async Task Create(Colleague colleague)
        {
            await _colleagueRepository.Create(colleague);
        }


        public async Task Delete(Colleague colleague)
        {
            await _colleagueRepository.Delete(colleague);
        }

        public async Task Edit(int id, Colleague colleague)
        {
            var existColleague = await _colleagueRepository.GetById(id);

            existColleague.Image = colleague.Image;

            await _colleagueRepository.Edit(existColleague);

        }

        public async Task<IEnumerable<Colleague>> GetAll()
        {
            return await _colleagueRepository.GetAll(); 
        }

        public async Task<Colleague> GetById(int id)
        {
            return await _colleagueRepository.GetById(id);
        }

    }
}
