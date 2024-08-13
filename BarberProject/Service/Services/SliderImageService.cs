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
    public class SliderImageService : ISliderImageService
    {
        private readonly ISliderImageRepository _sliderImageRepository;

        public SliderImageService(ISliderImageRepository sliderImageRepository)
        {
            _sliderImageRepository = sliderImageRepository;
        }
        public async Task Create(SliderImage sliderImage)
        {
            await _sliderImageRepository.Create(sliderImage);
        }

        public async Task Delete(SliderImage sliderImage)
        {
            await _sliderImageRepository.Delete(sliderImage);
        }

        public async Task DeleteImage(SliderImage image)
        {
            await _sliderImageRepository.DeleteImage(image);
        }

        public async Task Edit(int id, SliderImage sliderImage)
        {
            var existSliderImage = await _sliderImageRepository.GetById(id);

            existSliderImage.Image = sliderImage.Image;

            await _sliderImageRepository.Edit(existSliderImage);

        }

        public async Task<IEnumerable<SliderImage>> GetAll()
        {
            return await _sliderImageRepository.GetAll();
        }

        public async Task<SliderImage> GetById(int id)
        {
            return await _sliderImageRepository.GetById(id);
        }
    }
}
