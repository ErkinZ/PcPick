using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PcPick.ViewModels
{
    //CategoryEditViewModel to hanndle category edit, create and delete.
    //Every ErrorMessage is written in english because my site is in english.
    public class CategoryEditViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
    }
}