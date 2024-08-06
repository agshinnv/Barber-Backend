using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Users
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = ("Email address is not valid"))]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
