using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PcPick.ViewModels
{
    public class CategoryIndexViewModel
    {
        public CategoryIndexViewModel()
        {
            CategoriesList = new List<CategoryListViewModel>();
        }

        public class CategoryListViewModel
        {
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        public List<CategoryListViewModel> CategoriesList { get; set; }
    }
}