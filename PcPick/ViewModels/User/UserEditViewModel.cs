using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcPick.ViewModels
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        public string UserRoles { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string UserDropDownHolder { get; set; }
        public List<SelectListItem> UserDropDownList { get; set; }
    }
}