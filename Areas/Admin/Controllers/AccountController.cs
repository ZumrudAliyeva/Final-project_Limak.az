using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Limak.az.Contexts;
using Limak.az.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Limak.az.Areas.ViewModels;
using Limak.az.ViewModels;

namespace Limak.az.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class AccountController : Controller
    {
        #region fields and ctor
        private readonly LimakDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<CustomAppUser> _userManager;
        private readonly SignInManager<CustomAppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(LimakDbContext context, IMapper mapper,
               UserManager<CustomAppUser> userManager, SignInManager<CustomAppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion
 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserList", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Email ve ya şifrə yanlışdır");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Istifadəçi tapılmadı");
                }
            }


            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult CreateManager()
        {
            ViewBag.Roles = _context.Roles.Select(
              c => new SelectListItem()
              {
                  Text = c.Name,
                  Value = c.Id.ToString()
              }).ToList();

            return View();
        }

  

        [HttpPost]
        public async Task<IActionResult> CreateManager(AdminRegisterViewModel manager, string RoleId)
        {
            ViewBag.Roles = _context.Roles.Select(
              c => new SelectListItem()
              {
                  Text = c.Name,
                  Value = c.Id.ToString()
              }).ToList();

            if (ModelState.IsValid)
            {
                CustomAppUser newmanager = new CustomAppUser
                {
                    Email = manager.Email,
                    UserName = manager.Email,
                    EmailConfirmed = true,
                };

                IdentityResult identityResult = await _userManager.CreateAsync(newmanager, manager.Password);

                if (identityResult.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(RoleId);
                    if (role!=null)
                    {
                        await _userManager.AddToRoleAsync(newmanager, role.Name);
                        return RedirectToAction("Managers", "Home");
                    }
                      
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Role doesn't exist");
                    }
                   
                }
                else
                {
                    IEnumerable<IdentityError> identityErrors = identityResult.Errors;
                
            }

            }
            return View();
        }

        public IActionResult DeleteManager(string id)
        {
            var manager = _context.Users.FirstOrDefault(x => x.Id == id);
            if (manager!=null)
            {
                _context.Users.Remove(manager);
                _context.SaveChanges();
            }
            return RedirectToAction("Managers");
        }
    }

}
