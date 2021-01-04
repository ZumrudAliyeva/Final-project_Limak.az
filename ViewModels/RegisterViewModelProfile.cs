using AutoMapper;
using Limak.az.Models;
using Limak.az.ViewModels;

namespace Limak.ViewModels
{
    public class RegisterViewModelProfile : Profile
    {
        public RegisterViewModelProfile()
        {
            CreateMap<RegisterViewModel, CustomAppUser>();
            CreateMap<CustomAppUser,RegisterViewModel>();
        }
    }
}
