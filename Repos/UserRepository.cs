using AutoMapper;
using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.az.Repos
{
    public class UserRepository : IUserRepository
    {
        #region fields
        private readonly LimakDbContext _limakDbContext;
        private readonly SignInManager<CustomAppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<CustomAppUser> _userManager;
        private readonly IUserBalanceRepository _userBalance;
        #endregion

        #region ctor
        public UserRepository(LimakDbContext limakDbContext, IMapper mapper,
            UserManager<CustomAppUser> userManager,
            SignInManager<CustomAppUser> signInManager,
               IUserBalanceRepository userBalance)
        {
            _limakDbContext = limakDbContext;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userBalance = userBalance;
        }
        #endregion
        public async Task<IdentityResult> Create(RegisterViewModel userViewModel)
        {
            var user = _mapper.Map<CustomAppUser>(userViewModel);
            user.UserName = userViewModel.Email;
            user.CustomerId = Guid.NewGuid().ToString().Substring(0, 7);

            var result = await _userManager.CreateAsync(user, userViewModel.Password);

            UserBalance balanceUSD = new UserBalance()
            {
                CurrencyId = 1,
                Balance = 0,
                UserId = user.Id
            };
            UserBalance balanceTRY = new UserBalance()
            {
                CurrencyId = 2,
                Balance = 0,
                UserId = user.Id
            };
            UserBalance balanceAZN = new UserBalance()
            {
                CurrencyId = 3,
                Balance = 0,
                UserId = user.Id
            };
            _userBalance.Create(balanceUSD);
            _userBalance.Create(balanceTRY);
            _userBalance.Create(balanceAZN);

            return result;
        }

        public RegisterViewModel GetById(string id)
        {
            var user = _limakDbContext.Users.Find(id);
            return (RegisterViewModel)Convert.ChangeType(user, typeof(RegisterViewModel));
        }


        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                loginViewModel.Email, loginViewModel.Password, false, false);


            return signInResult;
        }


        public async Task<IdentityResult> Update(SettingsViewModel viewModel)
        {
            var _muser = _limakDbContext.Users.FirstOrDefault(x => x.Id == viewModel.Id);
            var user = _mapper.Map(viewModel, _muser);
            user.UserName = viewModel.Email;
            var newPassword = _userManager.PasswordHasher.HashPassword(user, viewModel.Password);
            user.PasswordHash = newPassword;

            var result = await _userManager.UpdateAsync(user);


            return result;
        }
    }
}
