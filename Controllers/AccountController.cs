using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Limak.az.Contexts;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Limak.az.Controllers
{
    public class AccountController : Controller
    {
        #region fields and ctor

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LimakDbContext _db;
        private readonly UserManager<CustomAppUser> userManager;
        private readonly SignInManager<CustomAppUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(IMapper mapper, IUserRepository userRepository, RoleManager<IdentityRole> roleManager,
            LimakDbContext db, UserManager<CustomAppUser> userManager, SignInManager<CustomAppUser> signInManager, ILogger<AccountController> logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.Create(userViewModel);

                if (result.Succeeded)
                {
                    var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == userViewModel.Email);
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { email = user.Email, emailConfirmationToken = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, confirmationLink);

                    //send confirmation link by smtp//

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("limakmmctest@gmail.com", "Password_1303");

                    MailMessage message = new MailMessage(new MailAddress("limakmmctest@gmail.com"), new MailAddress(userViewModel.Email));
                    client.EnableSsl = true;
                    message.IsBodyHtml = true;
                    message.Subject = "Email Confirmation";
                    client.TargetName = "Limak MMC";

                    message.Body = "<form action='" + confirmationLink + "'>" +
                        "<input name='Token' value='" + token + "' type='hidden'/>" +
                        "<input name='Email' value='" + user.Email + "' type='hidden'/>" +
                       " <div style = 'width: 600px; display: flex; align-items: center; justify-content: center;' >" +
                       " <div style ='font-size: 15px;text-align: center;'>" +
                        "<h3> Emailinizi təsdiqləyin</h3>" +
                       " <p> Hörmətli  " + userViewModel.Name + " " + userViewModel.Surname + ", </p> " +
                       "<p>Limak.az saytındakı qeydiyyatınızı təsdiqləmək üçün zəhmət olmasa klikləyin.</p> " +
                        "<input style='cursor: pointer; font-weight: 800; font-size: 16px; color: white; background-color: #f95732; padding: 14px 24px; border-radius: 5px; border: #f95732;'  type='submit' value='EmailConfrimation'/></form>" +
                     " </div> </div>";

                    await client.SendMailAsync(message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }

            }
            return View(userViewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (email == null || token == null)
            {
                return View("Error");
            }

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                TempData["Message"] = "Email yanlışdır";
                return RedirectToAction("Logout", "Account");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = "Email yanlışdır";
            return RedirectToAction("Logout", "Account");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                await signInManager.SignOutAsync();
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == loginViewModel.Email);
                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        var result = await _userRepository.Login(loginViewModel);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        else
                        {
                            ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email təsdiq edilməyib");
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Email yanlışdır");
                }
            }
            return PartialView("_LoginPartial", loginViewModel);
        }


        [HttpGet]
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Settings(string id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            SettingsViewModel settingsViewModel = new SettingsViewModel();
            settingsViewModel = _mapper.Map<SettingsViewModel>(user);
            return View(settingsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(SettingsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.Update(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(viewModel);
        }

        //public static string GetRandomPassword()
        //{
        //    string chars = "0123456789";

        //    StringBuilder sb = new StringBuilder();
        //    Random random = new Random();

        //    for (int i = 0; i < 7; i++)
        //    {
        //        int index = random.Next(chars.Length);
        //        sb.Append(chars[index]);
        //    }

        //    return sb.ToString();
        //}

    }
}
