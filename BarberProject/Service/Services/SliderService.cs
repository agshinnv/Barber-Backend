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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public async Task Create(Slider slider)
        {
            await _sliderRepository.Create(slider);
        }

        public async Task DeleteAsync(Slider slider)
        {
            await _sliderRepository.Delete(slider);
        }

        public async Task EditAsync(int id, Slider slider)
        {
            var existSlider = await GetByIdAsync(id);

            existSlider.SliderTitle = slider.SliderTitle;
            existSlider.SliderDescription = slider.SliderDescription;
            existSlider.SliderImages = slider.SliderImages;

            await _sliderRepository.Edit(existSlider);
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _sliderRepository.GetAll();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _sliderRepository.GetById(id);
        }
    }
}
