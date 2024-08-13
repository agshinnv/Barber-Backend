using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderImageService
    {
        Task<IEnumerable<SliderImage>> GetAll();
        Task<SliderImage> GetById(int id);
        Task Create(SliderImage sliderImage);
        Task Delete(SliderImage sliderImage);
        Task Edit(int id, SliderImage sliderImage);
        Task DeleteImage(SliderImage image);
    }
}
