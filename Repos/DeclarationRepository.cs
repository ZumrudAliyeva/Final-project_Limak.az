using AutoMapper;
using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.az.Repos
{
    public class DeclarationRepository : IDeclarationRepository
    {
        private readonly LimakDbContext _context;
        private readonly IMapper _mapper;

        public DeclarationRepository(LimakDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Create(DeclarationViewModel declarationViewModel)
        {
            var declaration = _mapper.Map<Declaration>(declarationViewModel);
            await _context.Declarations.AddAsync(declaration);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task Delete(int id)
        {
            var declaration = await _context.Declarations.FindAsync(id);
            _context.Declarations.Remove(declaration);
            await _context.SaveChangesAsync();
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Pay(int id, string userId)
        {
            var currencyId = 0;
            var declaration = _context.Declarations.Find(id);
            if (declaration.CountryId == 2)
            {
                currencyId = 2;
            }
            else
            {
                currencyId = 1;
            }
            var result = false;
            var userbalance = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);
            var balance = userbalance.Balance;
            var amount = declaration.ShippingPrice;
            if (declaration.CountryId == 2)
            {
                if (declaration.ProductPrice > 0 && declaration.ProductWeight <= 0.25m)
                {
                    amount = 2.00m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.25m && declaration.ProductWeight <= 0.5m)
                {
                    amount = 3.00m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.5m && declaration.ProductWeight <= 0.7m)
                {
                    amount = 4.00m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.7m && declaration.ProductWeight <= 1m)
                {
                    amount = 4.5m * declaration.Quantity;
                }
                else if (declaration.ProductWeight <= 1m)
                {
                    amount = 4.5m * declaration.Quantity;
                }
            }
            else
            {
                if (declaration.ProductPrice > 0 && declaration.ProductWeight <= 0.25m)
                {
                    amount = 1.99m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.25m && declaration.ProductWeight <= 0.5m)
                {
                    amount = 2.99m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.5m && declaration.ProductWeight <= 0.7m)
                {
                    amount = 3.99m * declaration.Quantity;
                }
                else if (declaration.ProductPrice > 0.7m && declaration.ProductWeight <= 1m)
                {
                    amount = 5.99m * declaration.Quantity;
                }
                else if(declaration.ProductWeight <= 1m)
                {
                    amount = 5.99m * declaration.Quantity;
                }
            }

            if (declaration.DeclarationStatusId == 1)
            {
                if (amount <= balance)
                {
                    declaration.DeclarationStatusId = 2;
                    _context.Declarations.Update(declaration);
                    userbalance.Balance = balance - amount;
                    _context.UserBalances.Update(userbalance);
                    _context.SaveChanges();

                    result = true;

                }
            }

            return result;
        }
    }
}
