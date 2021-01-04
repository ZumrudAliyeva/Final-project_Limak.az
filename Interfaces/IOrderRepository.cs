using Limak.az.Models;
using Limak.az.ViewModels;
using System.Threading.Tasks;

namespace Limak.az.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderViewModel orderViewModel);
        Task Delete(int id);
        bool Pay(int id, string userId);
        Task<Order> GetOrderById(int id);

    }
}
