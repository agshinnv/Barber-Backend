using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWorkTimeService
    {
        Task<IEnumerable<WorkTime>> GetAll();
        Task<WorkTime> GetById(int id);
        Task Delete(WorkTime workTime);
        Task Create(WorkTime workTime);
        Task Edit(int id, WorkTime workTime);
    }
}
