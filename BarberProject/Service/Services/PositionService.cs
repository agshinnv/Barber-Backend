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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public async Task Create(Position position)
        {
            await _positionRepository.Create(position);
        }

        public async Task Delete(Position position)
        {
            await _positionRepository.Delete(position);
        }

        public async Task Edit(int id, Position position)
        {
            var existPosition = await _positionRepository.GetById(id);

            existPosition.Name = position.Name;

            await _positionRepository.Edit(existPosition);
        }

        public async Task<IEnumerable<Position>> GetAll()
        {
            return await _positionRepository.GetAll();
        }

        public async Task<Position> GetById(int id)
        {
            return await _positionRepository.GetById(id);
        }
    }
}
