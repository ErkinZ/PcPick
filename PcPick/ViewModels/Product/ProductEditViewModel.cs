using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcPick.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public List<SelectListItem> CategoryDropDownList { get; set; }
    }
}