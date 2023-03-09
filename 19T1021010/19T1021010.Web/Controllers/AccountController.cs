using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _19T1021010.BusinessLayers;
using _19T1021010.DomainModels;

namespace _19T1021010.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName = "", string password = "")
        {
            try 
            { 
                if (Request.HttpMethod == "GET")
                {
                    return View();
                }

                ViewBag.UserName = userName;
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {

                    ModelState.AddModelError("LoginError", "Vui lòng nhập đủ thông tin");
                    return View();
                }

                var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
                if (userAccount == null)
                {
                    ModelState.AddModelError("LoginError", "Đăng nhập thất bại");
                    return View();
                }
                
                string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
                FormsAuthentication.SetAuthCookie(cookieValue, false);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau :))");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            var userAccount = Converter.CookieToUserAccount(User.Identity.Name);
            try {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || 
                    string.IsNullOrWhiteSpace(confirmPassword))
                {
                    ViewBag.PasswordErr = "Vui lòng nhập đủ thông tin";
                    return View();
                }
                else if (oldPassword.Equals(userAccount.Password))
                {
                    if (confirmPassword.Equals(newPassword))
                    {
                        UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);

                        var newUserAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, newPassword);
                        string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(newUserAccount);
                        FormsAuthentication.SetAuthCookie(cookieValue, false);
                        ViewBag.PasswordErr = "Đổi mật khẩu thành công";
                        return View();
                    }
                    else
                    {
                        ViewBag.PasswordErr = "Xác nhận mật khẩu sai";
                        return View();
                    }
                
                }
                else
                {
                    ViewBag.PasswordErr = "Mật khẩu không đúng";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau :))");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}