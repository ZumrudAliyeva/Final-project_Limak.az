using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Final_project_Limak.az.Contexts;
using Final_project_Limak.az.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Final_project_Limak.az.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomAppUser> userManager;
        private readonly SignInManager<CustomAppUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly CustomAppDbContext context;

        public AccountController(UserManager<CustomAppUser> userManager,
            SignInManager<CustomAppUser> signInManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

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
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                CustomAppUser user = new CustomAppUser()
                {
                    
                    Name = registerView.Name,
                    UserName = registerView.Name,
                    Surname = registerView.Surname,
                    Email = registerView.Email,
                    PhoneNumber = registerView.Phone,
                    IDSerialNumber = registerView.IDSerialNumber,
                    Citizenship = registerView.Citizenship,
                    FINkode = registerView.FINkode,
                    Birthday = registerView.Birthday,
                    Birthmonth = registerView.Birthmonth,
                    Birthyear = registerView.Birthyear,
                    Gender = registerView.Gender,
                    Address = registerView.Address
                };

                var result = userManager.CreateAsync(user, registerView.Password).Result;

                if (result.Succeeded)
                {
                    var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                   new { userId = user.Id, emailConfirmationToken = emailConfirmationToken }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);

                    //send confirmation link by smtp//

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("limakmmc.test@gmail.com", "Password_1303");

                    MailMessage message = new MailMessage(new MailAddress("limakmmc.test@gmail.com"), new MailAddress(registerView.Email));
                    client.EnableSsl = true;
                    message.IsBodyHtml = true;
                    message.Subject = "Email Confirmation";
                    client.TargetName = "Limak MMC";

                    message.Body = @"<html><body><p><strong><div><div>Hi Sir/ Madam. We just need to verify your Email address for complete your registration!<br>click here <a href='https://localhost:44380/Account/ConfirmEmail?userId={user.Id}&emailConfirmationToken={emailConfirmationToken}'>Hesabını təsdiqlə</a></div></div></strong></p></body></html>";

                    await client.SendMailAsync(message);
                    //send confirmation link by smtp//


                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Admin");
                    //}

                    //ViewBag.ErrorTitle = "Registration successful";
                    //ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                    //        "email, by clicking on the confirmation link we have emailed you";
                    //return View("Error");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(registerView);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string emailConfirmationToken)
        {
            if (userId == null || emailConfirmationToken == null)
            {
                return View("Error");
            }

            CustomAppUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;

                return RedirectToAction("Index", "Home");
            }

            return View("Error");
        }


        [HttpGet]
        public IActionResult Login(LoginViewModel loginView)
        {
            return View(loginView);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterViewModel subViewModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(subViewModel.Email);

                if (user != null && !user.EmailConfirmed &&
                            (await userManager.CheckPasswordAsync(user, subViewModel.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(subViewModel);
                }

                var result = await signInManager.PasswordSignInAsync(
                    subViewModel.Email,
                    subViewModel.Password,
                    subViewModel.LoginViewModel.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }

            return View(subViewModel);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
