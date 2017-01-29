using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class RateSongModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SongId { get; set; }

        public int Rate { get; set; }
    }
}