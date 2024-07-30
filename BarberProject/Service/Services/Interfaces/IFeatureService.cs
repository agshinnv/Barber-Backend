using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<IEnumerable<Feature>> GetAll();
        Task<Feature> GetById(int id);  
        Task Delete(Feature feature);
        Task Create(Feature feature);
        Task Edit(int id, Feature feature);
    }
}
