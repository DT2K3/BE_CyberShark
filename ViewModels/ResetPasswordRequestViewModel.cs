using System.ComponentModel.DataAnnotations;

namespace BE_CyberShark.ViewModels
{
    public class ResetPasswordRequestViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
