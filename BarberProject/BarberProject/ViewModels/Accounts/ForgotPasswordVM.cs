using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Accounts
{
	public class ForgotPasswordVM
	{
		[Required]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
