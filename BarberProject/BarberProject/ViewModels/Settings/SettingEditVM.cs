using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Settings
{
    public class SettingEditVM
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
