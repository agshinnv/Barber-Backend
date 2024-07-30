using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<IEnumerable<ComplaintSuggest>> GetAll();
        Task<ComplaintSuggest> GetById(int id);
        Task Delete(ComplaintSuggest suggest);
        Task Create(ComplaintSuggest suggest);
    }
}
