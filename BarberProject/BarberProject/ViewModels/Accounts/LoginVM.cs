using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
