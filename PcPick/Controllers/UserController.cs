using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcPick.ViewModels;
using PcPick.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace PcPick.Controllers
{
    public class UserController : Controller
    {
        //Added
        ApplicationDbContext context;
        //Added
        public UserController()
        {
            context = new ApplicationDbContext();
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = new UserIndexViewModel();

            using (var db = new ApplicationDbContext())
            {
                model.UserList.AddRange(db.Users.Select(x => new UserIndexViewModel.UserListViewModel
                {
                    UserId = x.Id,
                    Email = x.Email,
                    UserName = x.UserName
                }));
                foreach (var item in model.UserList)
                {
                    item.UserRoles = UserManager.GetRoles(item.UserId).SingleOrDefault();
                }
                return View(model);
            }
        }

        #region Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            var model = new UserEditViewModel();
            var user = UserManager.FindById(id);
            using (var db = new ApplicationDbContext())
            {
                model.UserId = user.Id;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.UserRoles = UserManager.GetRoles(user.Id).SingleOrDefault();
                model.UserDropDownList = new List<SelectListItem>();

                foreach (var item in db.Roles)
                {
                    model.UserDropDownList.Add(new SelectListItem { Value = item.Name, Text = item.Name });
                }

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new ApplicationDbContext())
            {
                var user = UserManager.FindById(model.UserId);
                user.Id = model.UserId;

                UserManager.RemoveFromRole(user.Id, model.UserRoles);
                UserManager.AddToRole(user.Id, model.UserDropDownHolder);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        //[Authorize]
        //[HttpGet]
        //public ActionResult Delete(string id)
        //{
        //    var model = new UserEditViewModel();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var user = UserManager.FindById(id);
                
        //        return View(model);
        //    }
        //}

        //[Authorize]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var obj = db.Products.Find(id);
        //        if (obj != null)
        //        {
        //            db.Products.Remove(obj);
        //            db.SaveChanges();
        //            return RedirectToAction("Index", "Product", new { id = obj.CategoryId });
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        #endregion
    }
}