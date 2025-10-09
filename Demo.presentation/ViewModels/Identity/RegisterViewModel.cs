using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username can't be null!")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name can't be null!")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name can't be null!")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsAgreed { get; set; }
    }
}
