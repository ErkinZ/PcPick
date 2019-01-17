using PcPick.MyDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PcPick.ViewModels;

namespace PcPick.Controllers
{
    public class HomeController : Controller
    {
        //private MyDbContext db = new MyDbContext();

        // GET: Home
        public ActionResult Index()
        {
            var model = new CategoryIndexViewModel();
            using (var db = new MyDbContext())
            {
                model.CategoriesList.AddRange(db.Categories.Select(x => new CategoryIndexViewModel.CategoryListViewModel
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name
                }));

                return View(model);
            }
        }

        //My edit region where all the edit requests contain
        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new MyDbContext())
            {
                var cat = db.Categories.Find(id);
                var model = new CategoryEditViewModel
                {
                    CategoryId = cat.CategoryId,
                    Name = cat.Name
                };
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new MyDbContext())
            {
                var cat = db.Categories.FirstOrDefault(x => x.CategoryId == model.CategoryId);

                cat.CategoryId = model.CategoryId;
                cat.Name = model.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        #endregion

        //My create region where all the create requests contain
        #region Create

        #endregion

        //My delete region where all the delete requests contain
        #region Delete

        #endregion
    }
}