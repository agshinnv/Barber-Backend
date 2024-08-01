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
    public class WorkTimeService : IWorkTimeService
    {
        private readonly IWorkTimeRepository _workTimeRepository;

        public WorkTimeService(IWorkTimeRepository workTimeRepository)
        {
            _workTimeRepository = workTimeRepository;
        }
        public async Task Create(WorkTime workTime)
        {
            await _workTimeRepository.Create(workTime);
        }

        public async Task Delete(WorkTime workTime)
        {
            await _workTimeRepository.Delete(workTime);    
        }

        public async Task Edit(int id, WorkTime workTime)
        {
            var existWorkTime = await _workTimeRepository.GetById(id);

            existWorkTime.WorkDay = workTime.WorkDay;
            existWorkTime.WorkHour = workTime.WorkHour;

            await _workTimeRepository.Edit(existWorkTime);

        }

        public async Task<IEnumerable<WorkTime>> GetAll()
        {
            return await _workTimeRepository.GetAll();  
        }

        public async Task<WorkTime> GetById(int id)
        {
            return await _workTimeRepository.GetById(id);
        }
    }
}
