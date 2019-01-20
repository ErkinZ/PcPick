using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcPick.ViewModels;
using PcPick.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PcPick.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            //var model = new UserIndexViewModel();
            //var role = new IdentityDbContext();

            //using (var db = new IdentityDbContext())
            //{
            //    model.UserList.AddRange(db.Users.Select(x => new UserIndexViewModel.UserListViewModel
            //    {
            //        UserId = x.Roles.Select(z => z.UserId),
            //        Email = x.Email,
            //        UserName = x.UserName,
            //        UserRoles = db.Roles.Select(x => x.Name)
            //    }));
            //}
            using (var db = new ApplicationDbContext())
            {
                var model = (from user in db.Users
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          Email = user.Email,
                                          RoleNames = (from userRole in user.Roles
                                                       join role in db.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Select(p => new UserIndexViewModel()
                                      {
                                          UserId = p.UserId,
                                          UserName = p.Username,
                                          Email = p.Email,
                                          UserRoles = string.Join(",", p.RoleNames)
                                      });
                return View(model);
            }
        }
    }
}