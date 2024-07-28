using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Sliders
{
    public class SliderEditVM
    {
        [Required]
        public string SliderTitle { get; set; }
        public string SliderDescription { get; set; }
        public List<SliderEditImageVM> ExistImages { get; set; }
        public List<IFormFile> NewSliderImages { get; set; }
    }
}
