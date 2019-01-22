using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcPick.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserRoles { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [EmailAddress(ErrorMessage = "You need to fill in a email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserName { get; set; }

        public string UserDropDownHolder { get; set; }
        public List<SelectListItem> UserDropDownList { get; set; }
    }
}