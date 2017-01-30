using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}