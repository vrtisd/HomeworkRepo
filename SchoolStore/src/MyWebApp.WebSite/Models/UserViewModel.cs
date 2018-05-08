using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.WebSite.Models
{
    public class UserViewModel
    {
        public UserModel User { get; set; }
        public ClassModel[] Classes { get; set; }
    }
}