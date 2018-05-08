﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApp.WebSite.Models
{
    public class LoginModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email Address")]
        //public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
    }
}