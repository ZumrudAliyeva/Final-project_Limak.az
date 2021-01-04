using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using System.Linq;

namespace Limak.az.Repos
{
    public class UserBalanceRepository : IUserBalanceRepository
    {
        private readonly LimakDbContext _context;

        public UserBalanceRepository(LimakDbContext context)
        {
            _context = context;
        }

        public void Create(UserBalance balance)
        {
            _context.UserBalances.Add(balance);
            _context.SaveChanges();

        }

        public void Increase(string userId, int currencyId, decimal amount)
        {
            var userbalance = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);
            if (userbalance != null)
            {
                userbalance.Balance += amount;
                _context.UserBalances.Update(userbalance);
                _context.SaveChanges();
            }
        }

        public void Decrease(string userId, int currencyId, decimal amount)
        {
            var userbalance = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);
            if (userbalance != null)
            {
                userbalance.Balance -= amount;
                _context.UserBalances.Update(userbalance);
                _context.SaveChanges();
            }
        }


    }
}
