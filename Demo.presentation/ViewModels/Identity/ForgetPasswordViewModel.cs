using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email cannot be empty")]
        public string Email { get; set; }
    }
}
