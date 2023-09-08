using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSNews.Models.ViewModels;
using System.Web.Security;
using CMSNewModels.Context;
using CMSNewService.Service;

namespace CMSNews.Controllers
{

    
    public class AccountController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        private UserService _userService;
        public AccountController()
        {
            _userService = new UserService(db);
        }

        public ActionResult Login(string returnUrl = "/")
        {
            LoginViewModel login = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "MobileNumber,Password,ReturnUrl")] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
               var user= _userService.GetAll().FirstOrDefault(t => t.MobileNumber == login.MobileNumber && t.Password == login.Password);
                if (user !=null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(login.MobileNumber, login.RemmemberPassword);
                        return Redirect(login.ReturnUrl);
                    }
                    ModelState.AddModelError("MobileNumber", "حساب کاربری شما فعال نمیباشد");

                }
                ModelState.AddModelError("MobileNumber", "نام کاربری یا رمز عبور شما اشتباه است");
                return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        public ActionResult loginState()
        {
            ViewBag.LoginState = false;
            if (User.Identity.IsAuthenticated)
            {
                
                ViewBag.LoginState = true;
            }
            return PartialView();
        }
    }
}