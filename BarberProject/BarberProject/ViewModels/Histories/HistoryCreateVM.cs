using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Histories
{
    public class HistoryCreateVM
    {
        public string UpTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }

    }
}
