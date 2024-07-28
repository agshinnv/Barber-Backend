using Domain.Models;

namespace BarberProject.ViewModels.Abouts
{
    public class AboutDetailVM
    {
        public int Id { get; set; }
        public string AboutTitle { get; set; }
        public string AboutDesc { get; set; }
        public string AboutPro1 { get; set; }
        public string AboutPro2 { get; set; }
        public string AboutPro3 { get; set; }
        public List<AboutImage> AboutImages { get; set; }
    }
}
