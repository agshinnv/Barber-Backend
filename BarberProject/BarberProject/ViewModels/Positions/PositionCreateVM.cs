using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Positions
{
    public class PositionCreateVM
    {
        [Required]
        public string PositionName { get; set; }
    }
}
