using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BirthdayCard.Models
{
    public class Card
    {
        [Required(ErrorMessage = "Please enter \"From\" name")]
        public string From { get; set; }

        [Required(ErrorMessage = "Please enter \"To\" name")]
        public string To { get; set; }

        [Required(ErrorMessage = "Please enter a message")]
        public string Message { get; set; }
    }
}