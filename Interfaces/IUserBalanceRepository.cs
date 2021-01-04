using Limak.az.Models;

namespace Limak.az.Interfaces
{
    public interface IUserBalanceRepository
    {
        void Create(UserBalance balance);
        void Increase(string userId, int currencyId, decimal amount);
        void Decrease(string userId, int currencyId, decimal amount);

    }
}
