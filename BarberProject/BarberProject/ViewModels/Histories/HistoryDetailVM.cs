using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Histories
{
    public class HistoryDetailVM
    {
        public string UpTitle { get; set; }
        public string MainTitle { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
    }
}
