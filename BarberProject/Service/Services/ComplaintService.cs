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
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }
        public async Task Create(ComplaintSuggest suggest)
        {
            await _complaintRepository.Create(suggest);
        }

        public async Task Delete(ComplaintSuggest suggest)
        {
            await _complaintRepository.Delete(suggest);
        }

        public async Task<IEnumerable<ComplaintSuggest>> GetAll()
        {
            return await _complaintRepository.GetAll();
        }

        public async Task<ComplaintSuggest> GetById(int id)
        {
            return await _complaintRepository.GetById(id);
        }
    }
}
