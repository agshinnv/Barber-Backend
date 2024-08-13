using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Sliders
{
    public class SliderEditVM
    {
        [Required]
        public string SliderTitle { get; set; }
        [Required]
        public string SliderDescription { get; set; }
    }
}
