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
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task Create(Feature feature)
        {
            await _featureRepository.Create(feature);
        }

        public async Task Delete(Feature feature)
        {
            await _featureRepository.Delete(feature);
        }

        public async Task Edit(int id, Feature feature)
        {
            var existFeature = await _featureRepository.GetById(id);

            existFeature.ServiceName = feature.ServiceName;
            existFeature.Description = feature.Description;
            existFeature.Image = feature.Image;

            await _featureRepository.Edit(existFeature);

        }

        public async Task<IEnumerable<Feature>> GetAll()
        {
            return await _featureRepository.GetAll();
        }

        public async Task<Feature> GetById(int id)
        {
            return await _featureRepository.GetById(id);
        }
    }
}
