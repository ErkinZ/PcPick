using PcPick.MyDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcPick.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id, string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            
            using (var db = new MyDbContext())
            {
                model.CategoryName = string.Join("", db.Categories.Where(x => x.CategoryId == id).Select(x => x.Name));
                model.ProductList.AddRange(db.Products
                    .Select(x => new ViewModels.ProductIndexViewModel.ProductListViewModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        CategoryId = x.CategoryId
                    }).Where(x => x.CategoryId == id));

                model = Sort(model, sort);

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Search(string search, string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            model.Search = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                using (var db = new MyDbContext())
                {
                    model.ProductList.AddRange(db.Products
                        .Select(x => new ViewModels.ProductIndexViewModel.ProductListViewModel
                        {
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price
                        }));

                    model.ProductList = model.ProductList.Where(x => x.Name.ToUpper().Contains(search.ToUpper()) ||
                        x.Description.ToUpper().Contains(search.ToUpper())).ToList();

                    model = Sort(model, sort);

                    return View("Search", model);
                }
            }

            return View("Search", model);
        }

        //Method to sort products, the method takes in a model and a sort string as a parameter
        public ViewModels.ProductIndexViewModel Sort(ViewModels.ProductIndexViewModel model , string sort)
        {
            model.SortOrder = sort == "priceDesc" ? "priceAsc" : "priceDesc";
            model.SortOrder = sort == "nameDesc" ? "nameAsc" : "nameDesc";

            switch (model.SortOrder)
            {
                case "nameDesc":
                    model.ProductList = model.ProductList.OrderBy(x => x.Name).ToList();
                    break;

                case "priceAsc":
                    model.ProductList = model.ProductList.OrderBy(x => x.Price).ToList();
                    break;

                case "priceDesc":
                    model.ProductList = model.ProductList.OrderByDescending(x => x.Price).ToList();
                    break;

                default:
                    model.ProductList = model.ProductList.OrderByDescending(x => x.Name).ToList();
                    break;
            }
            return model;
        }
    }
}