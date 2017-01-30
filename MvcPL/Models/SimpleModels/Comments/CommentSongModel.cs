using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class CommentSongModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int SongId { get; set; }

        [Required]
        [Display(Name = "Comment ")]
        [StringLength(300, ErrorMessage = "{0} sould be at least {2} symbols long and not more then {1} ", MinimumLength = 1)]
        public string Text { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}