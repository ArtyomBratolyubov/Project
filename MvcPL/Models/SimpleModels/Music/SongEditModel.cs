using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SongEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name ")]
        [StringLength(50, ErrorMessage = "{0} sould be at least {2} symbols long and not more then {1}", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Description ")]
        [StringLength(500, ErrorMessage = "{0} should be not more then {1} symbols long")]
        public string Description { get; set; }

        [Display(Name = "Album ")]
        [Range(1, double.MaxValue, ErrorMessage = "Choose Album")]
        public int AlbumId { get; set; }

        [Display(Name = "Singer ")]
        public int SingerId { get; set; }

        public int? MusicId { get; set; }

        public int AuthorId { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}