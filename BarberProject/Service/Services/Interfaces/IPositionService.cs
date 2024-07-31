using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAll();
        Task<Position> GetById(int id);
        Task Delete(Position position);
        Task Create(Position position);
        Task Edit(int id, Position position);
    }
}
