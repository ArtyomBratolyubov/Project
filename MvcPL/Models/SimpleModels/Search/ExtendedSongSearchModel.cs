using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{

    public class ExtendedSongSearchModel
    {
        [Display(Name = "Name ")]
        public string Name { get; set; }

        [Display(Name = "Genres ")]
        public IEnumerable<int> SelectedGenres { get; set; }

        [Display(Name = "From ")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "To ")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "From ")]
        public int? RateFrom { get; set; }

        [Display(Name = "To ")]
        public int? RateTo { get; set; }

        public bool HasMusic { get; set; }
    }
}