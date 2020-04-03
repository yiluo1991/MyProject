using MyProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MyProject.OAServer.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult UserLogin(string username, string password)
        {
            MyProjectDbContext ctx = new MyProjectDbContext();
            var user = ctx.Users.FirstOrDefault(s => username == s.LoginName);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                //1.建立一个身份票据
                //  FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(username, true, FormsAuthentication.Timeout.Minutes);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, username, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, "管理员,会计");
                //2.加密，获得加密后的字符串
                var secretStr = FormsAuthentication.Encrypt(ticket);
                //3.写入到cookie中
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName)
                {
                    HttpOnly = true,
                    Path = FormsAuthentication.FormsCookiePath,
                    Expires = DateTime.Now.Add(FormsAuthentication.Timeout),
                    Value = secretStr
                });
                return Json(new
                {
                    success = true,
                    message = "登录成功"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "用户名或密码有误，请重新输入"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///     检查登录状态
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckLogin() {
            if (User.Identity.IsAuthenticated) {
                return Json(new
                {
                    success = true,
                    loginname = User.Identity.Name
                },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                },JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout() {
            FormsAuthentication.SignOut();
            return Json(new { success = true },JsonRequestBehavior.AllowGet);
        }
    }
}