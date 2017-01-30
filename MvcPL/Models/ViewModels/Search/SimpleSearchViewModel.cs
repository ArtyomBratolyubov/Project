using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SimpleSearchViewModel : BaseViewModel
    {
        public string Text { get; set; }

        public IEnumerable<SingerModel> Singers { get; set; }

        public IEnumerable<AlbumModel> Albums { get; set; }

        public IEnumerable<SongModel> Songs { get; set; }
    }
}