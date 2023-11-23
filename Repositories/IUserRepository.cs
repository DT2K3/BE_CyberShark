using BE_cybershark.Models;
using BE_CyberShark.ViewModels;

namespace BE_CyberShark.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);
        Task<User?> Create(RegisterViewModel registerViewModel);
        Task<string> Login(LoginViewModel loginViewModel);
    }
}
