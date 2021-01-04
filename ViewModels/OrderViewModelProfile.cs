using AutoMapper;
using Limak.az.Models;
using Limak.az.ViewModels;

namespace Limak.ViewModels
{
    public class OrderViewModelProfile:Profile
    {
        public OrderViewModelProfile()
        {
            CreateMap<OrderViewModel, Order>();
            CreateMap<Order, OrderViewModel>();
        }
    }
}
