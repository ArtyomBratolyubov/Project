using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SongModel : SongEditModel
    {

        public string SingerName { get; set; }

        public string GenreName { get; set; }

        public int GenreId { get; set; }

        public string AlbumName { get; set; }

        public double Rating { get; set; }

        public int CountOfRates { get; set; }
    }
}