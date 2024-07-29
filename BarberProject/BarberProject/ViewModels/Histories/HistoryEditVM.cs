using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Histories
{
    public class HistoryEditVM
    {
        [Required]
        public string UpTitle { get; set; }
        public string MainTitle { get; set; }
        [Required]
        public string Description { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
