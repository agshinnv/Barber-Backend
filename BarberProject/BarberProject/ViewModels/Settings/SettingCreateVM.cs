using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Settings
{
    public class SettingCreateVM
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
