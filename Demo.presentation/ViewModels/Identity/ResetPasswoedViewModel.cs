using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Identity
{
    public class ResetPasswoedViewModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
