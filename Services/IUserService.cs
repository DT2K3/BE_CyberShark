using BE_cybershark.Models;
using BE_CyberShark.ViewModels;

namespace BE_CyberShark.Services
{
    public interface IUserService
    {
        Task<User?> Register(RegisterViewModel registerViewModel);

        //jwt String
        Task<string> Login(LoginViewModel loginViewModel);

    }
}
