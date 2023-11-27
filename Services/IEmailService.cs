using BE_cybershark.Models;
using BE_CyberShark.ViewModels;

namespace BE_CyberShark.Services
{
    public interface IEmailService
    {
        Task SendEmailToCallback(ResetPasswordRequestViewModel emailAddress);
        Task ResetPasswordCallback(ResetPasswordViewModel model);
    }
}
