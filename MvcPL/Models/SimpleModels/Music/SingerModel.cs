﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SingerModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name ")]
        [StringLength(32, ErrorMessage = "{0} sould be at least {2} symbols long and not more then {1}", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Description ")]
        [StringLength(500, ErrorMessage = "{0} should be not more then {1} symbols long")]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Image ")]
        public int? ImageId { get; set; }

    }
}