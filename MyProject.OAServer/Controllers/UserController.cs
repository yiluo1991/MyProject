using MyProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.OAServer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
       /// <summary>
       /// 分页获取用户列表
       /// </summary>
       /// <param name="keyword"></param>
       /// <param name="page"></param>
       /// <param name="rows"></param>
       /// <returns></returns>
        public ActionResult GetList(string keyword, int page = 1, int rows = 10)
        {
            MyProjectDbContext ctx = new MyProjectDbContext();

            IQueryable<User> query;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                query = ctx.Users;
            }
            else
            {
                query = ctx.Users.Where(s => s.LoginName.Contains(keyword));
            }
            //一共有多少条符合条件的数据
            var total = query.Count();
            ////排序
            //query = query.OrderBy(s => s.Id);
            ////分页
            //query = query.Skip((page - 1) * rows).Take(rows);
            ////获取当前页的数据
            //var list = query.Select(s => new { s.LoginName }).ToList();
            var list=query.OrderBy(s=>s.Id).Skip((page - 1) * rows).Take(rows).Select(s => new { s.LoginName }).ToList();
            return Json(new
            {
                success = true,
                result = new
                {
                    total,
                    list
                }
            },JsonRequestBehavior.AllowGet);
        }
    }
}