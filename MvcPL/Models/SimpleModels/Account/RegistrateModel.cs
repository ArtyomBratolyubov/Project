using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class RegistrateModel
    {
        [Required]
        [Display(Name = "Login")]
        [StringLength(16, ErrorMessage = " {0} should be at least {2} symbols long", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = " {0} should be at least {2} symbols long", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
    }
}