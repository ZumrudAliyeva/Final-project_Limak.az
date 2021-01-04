using Limak.az.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Limak.az.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> Create(RegisterViewModel userViewModel);
        Task<SignInResult> Login(LoginViewModel loginViewModel);
        Task<IdentityResult> Update(SettingsViewModel viewModel);
        RegisterViewModel GetById(string id);


    }
}
