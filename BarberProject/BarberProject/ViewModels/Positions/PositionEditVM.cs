using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Positions
{
    public class PositionEditVM
    {
        [Required]
        public string PositionName { get; set; }
    }
}
