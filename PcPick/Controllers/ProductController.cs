using PcPick.MyDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcPick.ViewModels;

namespace PcPick.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id, string sort)
        {
            var model = new ProductIndexViewModel();
            
            using (var db = new MyDbContext())
            {
                model.CategoryName = string.Join("", db.Categories.Where(x => x.CategoryId == id).Select(x => x.Name));
                model.CategoryId = id;
                model.ProductList.AddRange(db.Products
                    .Select(x => new ProductIndexViewModel.ProductListViewModel
                    {
                        ProductId = x.ProductId,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        PhotoByte = x.Photo,
                        CategoryId = x.CategoryId
                    }).Where(x => x.CategoryId == id));

                model = Sort(model, sort);

                return View(model);
            }
        }

        public ActionResult ViewProduct(int? id)
        {
            var model = new ProductEditViewModel();

            using (var db = new MyDbContext())
            {
                var product = db.Products.Find(id);
                model.Name = product.Name;
                model.Description = product.Description;
                model.Price = product.Price;
                model.PhotoByte = product.Photo;
            }

            if (model.PhotoByte != null)
            {
                var base64 = Convert.ToBase64String(model.PhotoByte);
                model.PhotoString = string.Format($"data:image/gif;base64,{base64}");
            }

            return View(model);
        }

        #region Search
        [HttpGet]
        public ActionResult Search(string search, string sort)
        {
            var model = new ProductIndexViewModel();
            model.Search = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                using (var db = new MyDbContext())
                {
                    model.ProductList.AddRange(db.Products
                        .Select(x => new ProductIndexViewModel.ProductListViewModel
                        {
                            ProductId = x.ProductId,
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price,
                            CategoryId = x.CategoryId
                        }));

                    model.ProductList = model.ProductList.Where(x => x.Name.ToUpper().Contains(search.ToUpper()) ||
                        x.Description.ToUpper().Contains(search.ToUpper())).ToList();

                    model = Sort(model, sort);

                    return View("Search", model);
                }
            }

            return View("Search", model);
        }
        #endregion

        //My edit region where all the edit requests contain
        #region Edit
        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(int? id)
        {
            using (var db = new MyDbContext())
            {
                var prod = db.Products.Find(id);
                var model = new ProductEditViewModel
                {
                    ProductId = prod.ProductId,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    PhotoByte = prod.Photo,
                    CategoryId = prod.CategoryId
                };
                CategoriesDropDown(model);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(ProductEditViewModel model, HttpPostedFileBase img)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { id = model.CategoryId });
            }

            using (var db = new MyDbContext())
            {
                var prod = db.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

                if (img != null)
                {
                    model.PhotoByte = new byte[img.ContentLength];
                    img.InputStream.Read(model.PhotoByte, 0, img.ContentLength);
                }
                prod.ProductId = model.ProductId;
                prod.Name = model.Name;
                prod.Description = model.Description;
                prod.Price = model.Price;
                prod.Photo = model.PhotoByte;
                prod.CategoryId = model.CategoryId;


                db.SaveChanges();
                return RedirectToAction("Index", new { id = prod.CategoryId });
            }
        }
        #endregion

        //My create region where all the create requests contain
        #region Create
        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            var model = new ProductEditViewModel { CategoryId = (int)id };
            using (var db = new MyDbContext())
            {
                model.CategoryName = string.Join("" ,db.Categories.Where(x => x.CategoryId == id).Select(x => x.Name));
                model.CategoryId = (int)id;
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductEditViewModel model, HttpPostedFileBase img)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new MyDbContext())
            {
                if (img != null)
                {
                    model.PhotoByte = new byte[img.ContentLength];
                    img.InputStream.Read(model.PhotoByte, 0, img.ContentLength);
                }
                var prod = new Models.Product
                {
                    ProductId = model.ProductId,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Photo = model.PhotoByte,
                    CategoryId = model.CategoryId
                };

                db.Products.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index", "Product", new { id = model.CategoryId });
            }
        }
        #endregion

        //My delete region where all the delete requests contain
        #region Delete
        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (var db = new MyDbContext())
            {
                var prod = db.Products.Find(id);
                var model = new ProductEditViewModel
                {
                    ProductId = prod.ProductId,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    PhotoByte = prod.Photo,
                    CategoryId = prod.CategoryId
                };
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var db = new MyDbContext())
            {
                var obj = db.Products.Find(id);
                if (obj != null)
                {
                    db.Products.Remove(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product", new { id = obj.CategoryId });
                }
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        //Method to sort products, the method takes in a product and a sort string as a parameter
        //I use the first if statement there because i want the nameAsc to be seen in the url
        //instead of leaving it blank for ascending.
        public ProductIndexViewModel Sort(ProductIndexViewModel model, string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                sort = "nameAsc";
            }
            model.SortOrderName = sort == "nameDesc" ? "nameAsc" : "nameDesc";
            model.SortOrderPrice = sort == "priceDesc" ? "priceAsc" : "priceDesc";

            switch (sort)
            {
                case "nameAsc":
                    model.ProductList = model.ProductList.OrderBy(x => x.Name).ToList();
                    break;
                case "nameDesc":
                    model.ProductList = model.ProductList.OrderByDescending(x => x.Name).ToList();
                    break;
                case "priceAsc":
                    model.ProductList = model.ProductList.OrderBy(p => p.Price).ToList();
                    break;
                case "priceDesc":
                    model.ProductList = model.ProductList.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            return model;
        }

        //Method to generate a drop down meny that takes in a product parameter
        public void CategoriesDropDown(ProductEditViewModel model)
        {
            model.CategoryDropDownList = new List<SelectListItem>();
            using (var db = new MyDbContext())
            {
                foreach (var cat in db.Categories)
                {
                    model.CategoryDropDownList.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.Name });
                }
            }
        }
    }
}