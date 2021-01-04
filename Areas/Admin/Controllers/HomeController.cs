using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Limak.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin , Manager")]
    public class HomeController : Controller
    {
        #region fields and ctor

        private readonly LimakDbContext context;
        private readonly UserManager<CustomAppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDeclarationRepository _declarationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IWebHostEnvironment webHost;

        public HomeController(LimakDbContext context, UserManager<CustomAppUser> userManager,
            RoleManager<IdentityRole> roleManager,IDeclarationRepository declarationRepository,
             IOrderRepository orderRepository,IWebHostEnvironment webHost)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _declarationRepository = declarationRepository;
            _orderRepository = orderRepository;
            this.webHost = webHost;


            ViewBag.WarehouseId = context.Warehouses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
        }
        #endregion

      
        public IActionResult Index()
        {

            return View();
        }


        public IActionResult UserList()
        {
            List<CustomAppUser> users =null;

            var ur = context.UserRoles.ToList();
            foreach (var item in ur)
            {
               users  = context.Users.Where(x=>x.Id!=item.UserId).ToList();
            }
            return View(users);
        }

        public IActionResult UsersOrders(string id)
        {
            var orders = context.Orders.Where(c => c.User.Id == id).Include(c => c.Status).ToList();
            return View(orders);
        }

        public IActionResult UsersDeclarations(string id)
        {
            var declarations = context.Declarations.Where(c => c.User.Id == id).Include(c => c.DeclarationStatus).ToList();
            return View(declarations);
        }


        public IActionResult DeleteUser(string id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
        public IActionResult Managers()
        {
            var managers = new List<CustomAppUser>();
 
            var ur = context.UserRoles.ToList();

            foreach (var item in ur)
            {
               var manager= context.Users.FirstOrDefault(x => x.Id == item.UserId);

                managers.Add(manager);
            }

            return View(managers);
        }

        public IActionResult Orders()
        {
           var orders= context.Orders.Where(x => x.OrderStatusId != 0).Include(c => c.Status).Include(c => c.User).ToList();
            return View(orders);
        }

        [HttpPost]
        public IActionResult OrderDetails(int id)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == id);
            
            return View(order);
        }

        public IActionResult DeleteOrder(int id)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == id);
            context.Orders.Remove(order);
            context.SaveChanges();

            return RedirectToAction("Orders");
        }

        public IActionResult Declarations()
        {
            var declarations = context.Declarations.OrderBy(x => x.DeclarationStatusId).
                Include(c => c.User).Include(c => c.DeclarationStatus).ToList();
            return View(declarations);
        }

        [HttpPost]
        public IActionResult DeclarationDetails(int id)
        {
            var order = context.Declarations.FirstOrDefault(x => x.Id == id);

            return View(order);
        }
        public IActionResult CreateDeclaration(int id)
        {
            ViewBag.Warehouse = context.Warehouses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();

            ViewBag.Status = context.DeclarationStatuses.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();

            var order= context.Orders.FirstOrDefault(x=>x.Id==id);
            var declaration = new DeclarationViewModel();
            declaration.UserId = order.UserId;
            declaration.CountryId = order.CountryId;
            _orderRepository.Delete(order.Id);
            return View(declaration);
        }

        [HttpPost]
 
        public async Task<IActionResult> CreateDeclaration(DeclarationViewModel declarationViewModel)
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

            if (ModelState.IsValid)
            {
                declarationViewModel.FileName = newFileName;
                declarationViewModel.DeclarationDate = DateTime.Now;
                declarationViewModel.DeclarationStatusId = 1;
                declarationViewModel.TrackingCode = Guid.NewGuid().ToString().Substring(0, 8);
                var result = await _declarationRepository.Create(declarationViewModel);

                if (result == true)
                {
                    return RedirectToAction("Orders", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }
            }

            return View(declarationViewModel);

           }


        public IActionResult EditDeclaration(int id)
        {
            var declaration = context.Declarations.FirstOrDefault(x => x.Id == id);
            ViewBag.StatusId = context.DeclarationStatuses.Select(
               c => new SelectListItem()
               {
                   Text = c.Name,
                   Value = c.Id.ToString()
               }).ToList();

            return View(declaration);
        }

       
        [HttpPost]
        public IActionResult EditDeclaration(Declaration declaration)
        {
            ViewBag.StatusId = context.DeclarationStatuses.Select(
               c => new SelectListItem()
               {
                   Text = c.Name,
                   Value = c.Id.ToString()
               }).ToList();

                context.Declarations.Update(declaration);
                context.SaveChanges();
                return RedirectToAction("Declarations");

        }

       

    }
}
