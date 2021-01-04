using System.Security.Claims;
using Limak.az.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limak.az.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IUserBalanceRepository _userBalance;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BalanceController(IUserBalanceRepository userBalance,
                               IHttpContextAccessor httpContextAccessor)
        {
            _userBalance = userBalance;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult IncreaseBalance()
        {
            return View();
        }

        [HttpPost]

        public IActionResult IncreaseBalanceAzn(int currencyId, decimal amount)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _userBalance.Increase(userId, currencyId, amount);

            return RedirectToAction("UserPanel", "Home");
        }


        [HttpPost]

        public IActionResult IncreaseBalanceTl(int currencyId, decimal amount)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _userBalance.Increase(userId, currencyId, amount);

            return RedirectToAction("UserPanel", "Home");
        }

    }
}
