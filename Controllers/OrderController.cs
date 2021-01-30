using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Limak.Controllers
{
    public class OrderController : Controller
    {
        private readonly LimakDbContext context;
        private readonly IOrderRepository orderRepository;
        private readonly IDeclarationRepository _declarationRepository;
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(LimakDbContext context, IOrderRepository orderRepository, IDeclarationRepository declarationRepository,
            IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.orderRepository = orderRepository;
            _declarationRepository = declarationRepository;
            this.webHost = webHost;
            _httpContextAccessor = httpContextAccessor;

            ViewBag.Warehouse = context.Warehouses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
        }

        //public IEnumerable<Declaration> displayData { get; set; }

        //public async Task OnGet()
        //{
        //    displayData = (IEnumerable<Declaration>)await context.Declarations.Select(c => c.Warehouse.Name).ToListAsync();
        //}

        //public ActionResult GetWarehouse()
        //{
        //    return Json(context.Warehouses.Select(x => new
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }).ToList(), JsonRequestBehavior.AllowGet);
        //}



        public IActionResult CreateOrder()
        {
            ViewBag.Warehouse = context.Warehouses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
        {

            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var prPrice = orderViewModel.ProductPrice;
                var cargoPrice = orderViewModel.CargoPrice;
                var prResult = orderViewModel.PriceResult;
                orderViewModel.UserId = userId;
                orderViewModel.OrderStatusId = 1;
                orderViewModel.CountryId = 2;
                var amount = prPrice + cargoPrice;
                prResult = amount + (amount * (5m/100m));

                var result = await orderRepository.Create(orderViewModel);

                if (result == true)
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    var currencyId = 2;
                    if (orderViewModel.CountryId == 2)
                    {
                        currencyId = 2;
                    }
                    else
                    {
                        currencyId = 3;
                    }
                    //var result = false;
                    var userbalance = context.UserBalances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);
                    var balance = userbalance.Balance;

                    if (prResult <= balance)
                    {
                        userbalance.Balance = balance - prResult;
                        context.UserBalances.Update(userbalance);
                        context.SaveChanges();

                    }
                    ///////////////////////////////////////////////////////////////////////////////////
                    return RedirectToAction("UserPanel", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }
            }
            return View(orderViewModel);
        }


        //public IActionResult PayForOrder(int id)
        //{
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var result = orderRepository.Pay(id, userId);
        //    if (result == true)
        //    {
        //        return RedirectToAction("UserPanel", "Home");
        //    }
        //    else
        //    {
        //        TempData["message"] = "Balansda kifayət qədər vəsait yoxdur.";
        //        return RedirectToAction("UserPanel", "Home");
        //    }

        //}



        //[HttpGet]
        //public IActionResult DeleteOrder()
        //{
        //    return View();
        //}

        //[HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await orderRepository.Delete(id);

            return RedirectToAction("UserPanel", "Home");
        }



        //==== Declarations ====//

        public IActionResult CreateDeclaration()
        {
            ViewBag.Warehouse = context.Warehouses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeclaration(DeclarationViewModel declarationViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFileName = "";
                if (declarationViewModel.File != null)
                {
                    newFileName =
                 $"{Path.GetFileNameWithoutExtension(declarationViewModel.File.FileName)}" +
                 $"-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}" +
                 $"{Path.GetExtension(declarationViewModel.File.FileName)}";

                    var rootPath = Path.Combine(webHost.WebRootPath, "images", newFileName);
                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        declarationViewModel.File.CopyTo(fileStream);
                    }

                }

                declarationViewModel.FileName = newFileName;

                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var WarehouseId = declarationViewModel.ware;
                declarationViewModel.UserId = userId;
                declarationViewModel.CountryId = 2;
                //declarationViewModel.WarehouseId = declarationViewModel.
                //declarationViewModel.DeclarationDate = DateTime.Now;
                declarationViewModel.DeclarationStatusId = 1;
                //declarationViewModel.TrackingCode = Guid.NewGuid().ToString().Substring(0, 8);
                var result = await _declarationRepository.Create(declarationViewModel);

                if (result == true)
                {
                    return RedirectToAction("UserPanel", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }
            }

            return View(declarationViewModel);
        }



        public async Task<IActionResult> DeleteDeclaration(int id)
        {
            await _declarationRepository.Delete(id);

            return RedirectToAction("UserPanel", "Home");
        }

        public IActionResult PayForDeclaration(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _declarationRepository.Pay(id, userId);
            if (result == true)
            {
                return PartialView("_MyDeclarationsPartial", "Home");
            }
            else
            {
                TempData["message"] = "Balansda kifayət qədər vəsait yoxdur.";
                return RedirectToAction("UserPanel", "Home");
            }

        }

    }
}
