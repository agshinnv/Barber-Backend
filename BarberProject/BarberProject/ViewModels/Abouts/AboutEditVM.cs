using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Abouts
{
    public class AboutEditVM
    {
        [Required]
        public string AboutTitle { get; set; }
        [Required]
        public string AboutDesc { get; set; }
        public string AboutPro1 { get; set; }
        public string AboutPro2 { get; set; }
        public string AboutPro3 { get; set; }
        public List<AboutEditImageVM> ExistImages { get; set; }
        public List<IFormFile> NewAboutImages { get; set; }
    }
}
