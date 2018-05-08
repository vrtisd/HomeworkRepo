using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.WebSite.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public MenuModel(int id, string name, string displayText, string description)
        {
            Id = id;
            Name = name;
            DisplayText = displayText;
            Description = description;
        }
    }
}