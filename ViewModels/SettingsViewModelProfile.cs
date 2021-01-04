using AutoMapper;
using Limak.az.Models;
using Limak.az.ViewModels;

namespace Limak.ViewModels
{
    public class SettingsViewModelProfile : Profile
    {
        public SettingsViewModelProfile()
        {
            CreateMap<SettingsViewModel, CustomAppUser>();
            CreateMap<CustomAppUser, SettingsViewModel>();
        }
    }
}
