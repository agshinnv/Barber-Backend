using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IColleagueService
    {
        Task<IEnumerable<Colleague>> GetAll();
        Task<Colleague> GetById(int id);
        Task Delete(Colleague colleague);
        Task Create(Colleague colleague);
        Task Edit(int id, Colleague colleague);
    }
}
