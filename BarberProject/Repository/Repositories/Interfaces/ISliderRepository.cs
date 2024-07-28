using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ISliderRepository : IBaseRepository<Slider>
    {
        Task<Slider> GetByIdWithImages(int id);
        Task<IEnumerable<Slider>> GetAllWithImages();
        Task DeleteImage(SliderImage image);
    }
}
