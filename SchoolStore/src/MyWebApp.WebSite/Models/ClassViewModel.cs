using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.WebSite.Models
{
    public class ClassViewModel
    {        
        public ClassModel[] Classes { get; set; }

        [Required(ErrorMessage ="Please select a class")]
        public string SelectedClass { get; set; }
        public SelectList ClassSelectList { get; set; }
    }
}