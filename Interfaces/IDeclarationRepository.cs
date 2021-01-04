using Limak.az.Models;
using Limak.az.ViewModels;
using System.Threading.Tasks;

namespace Limak.az.Interfaces
{
    public interface IDeclarationRepository
    {

        Task<bool> Create(DeclarationViewModel declarationViewModel);
        Task Delete(int id);

        bool Pay(int id, string userId);
        Task<Order> GetOrderById(int id);

    }
}
