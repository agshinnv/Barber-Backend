using Domain.Models;

namespace BarberProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<About> Abouts { get; set; }
        public History History { get; set; }
    }
}
