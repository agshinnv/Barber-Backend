using Domain.Models;

namespace BarberProject.ViewModels.Sliders
{
    public class SliderDetailVM
    {
        public int Id { get; set; }
        public string SliderTitle { get; set; }
        public string SliderDesc { get; set; }
        public List<SliderImage> SliderImages { get; set; }

    }
}
