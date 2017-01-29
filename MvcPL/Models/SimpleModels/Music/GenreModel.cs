using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class GenreModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Genre ")]
        [StringLength(12, ErrorMessage = " {0} should be at least {2} symbols long and not more then {1}", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description ")]
        [StringLength(500, ErrorMessage = "{0} should be not more then {1} symbols long")]
        public string Description { get; set; }

        public int AuthorId { get; set; }

    }
}