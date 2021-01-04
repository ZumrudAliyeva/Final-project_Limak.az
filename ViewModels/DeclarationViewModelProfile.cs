using AutoMapper;
using Limak.az.Models;

namespace Limak.az.ViewModels
{
    public class DeclarationViewModelProfile : Profile
    {
        public DeclarationViewModelProfile()
        {
            CreateMap<DeclarationViewModel, Declaration>();
        }
    }
}
