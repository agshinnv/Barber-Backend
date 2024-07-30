using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Colleagues
{
    public class ColleagueCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
