using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Abouts
{
    public class AboutCreateVM
    {
        [Required]
        public List<IFormFile> AboutImages { get; set; }
        [Required]
        public string AboutTitle { get; set; }
        public string AboutDesc { get; set; }
        public string AboutPro1 { get; set; }
        public string AboutPro2 { get; set; }
        public string AboutPro3 { get; set; }
        
    }
}
