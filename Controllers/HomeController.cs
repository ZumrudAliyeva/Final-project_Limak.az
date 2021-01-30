using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Limak.az.Models;
using Microsoft.AspNetCore.Identity;
using Limak.az.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Limak.az.Controllers
{
    public class HomeController : Controller
    {
        #region fields and ctor
        private readonly ILogger<HomeController> _logger;
        private readonly LimakDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, LimakDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Questions()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Calculator()
        {
            return View();
        }

        public IActionResult Countries()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Shops()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }



        [Authorize]
        public IActionResult UserPanel()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                var allOrders = _context.Orders.Where(x => x.UserId == userId).ToList();
                ViewBag.AllOrders = allOrders.Count();

                var allDeclarations = _context.Declarations.Where(x => x.UserId == userId).ToList();
                var declaredDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 1).ToList();
                var inForeignWarehouseDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 2).ToList();
                var onTheWayDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 3).ToList();
                var inCustomsInspectionDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 3).ToList();
                var inLocalWarehouseDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 5).ToList();
                var byCourierDeliveryDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 5).ToList();
                var deliveredDeclarations = _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == 7).ToList();
                ViewBag.AllDeclarations = allDeclarations.Count();
                ViewBag.DeclaredDeclarations = declaredDeclarations.Count();
                ViewBag.InForeignWarehouseDeclarations = inForeignWarehouseDeclarations.Count();
                ViewBag.OnTheWayDeclarations = onTheWayDeclarations.Count();
                ViewBag.InCustomsInspectionDeclarations = inCustomsInspectionDeclarations.Count();
                ViewBag.InLocalWarehouseDeclarations = inLocalWarehouseDeclarations.Count();
                ViewBag.ByCourierDeliveryDeclarations = byCourierDeliveryDeclarations.Count();
                ViewBag.DeliveredDeclarations = deliveredDeclarations.Count();
            }

            if (_context.UserBalances.FirstOrDefault(x => x.UserId == userId) != null)
            {
                var balanceUSD = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == 1).Balance.ToString();
                var balanceTRY = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == 2).Balance.ToString();
                var balanceAZN = _context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == 3).Balance.ToString();
                ViewBag.BalanceTRY = balanceTRY;
                ViewBag.BalanceAZN = balanceAZN;
            }
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> GetBalanceAZN(int id)
        //{
        //    id = 3;

        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    var balances = new List<UserBalance>();

        //        balances = await _context.UserBalances.Where(x => x.UserId == userId && x.CurrencyId == 3).ToListAsync();

        //    return PartialView("_MyBalanceAZNPartial", balances);
        //}

        //[HttpPost]
        //public async Task<IActionResult> GetBalanceTL(int id)
        //{
        //    id = 2;

        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    var balances = new List<UserBalance>();

        //    balances = await _context.UserBalances.Where(x => x.UserId == userId && x.CurrencyId == 2).ToListAsync();

        //    return PartialView("_MyBalanceTLPartial", balances);
        //}


        [HttpPost]
        public async Task<IActionResult> GetAllOrdersWithStatusId(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orders = new List<Order>();

            if (id == 0)
            {
                orders = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
            }
            else
            {
                orders = await _context.Orders.Where(x => x.UserId == userId && x.OrderStatusId == id).ToListAsync();
            }
            return PartialView("_MyOrdersPartial", orders);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetAllDeclarationsWithStatusId(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var declarations = new List<Declaration>();

            if (id == 0)
            {
                declarations = await _context.Declarations.Where(x => x.UserId == userId).ToListAsync();
            }
            else
            {
                declarations = await _context.Declarations.Where(x => x.UserId == userId && x.DeclarationStatusId == id).ToListAsync();
            }
            return PartialView("_MyDeclarationsPartial", declarations);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
