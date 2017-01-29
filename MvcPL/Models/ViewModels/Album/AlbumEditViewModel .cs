using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class AlbumEditViewModel : BaseViewModel
    {
        public AlbumEditModel Album { get; set; }

        public IEnumerable<SingerModel> Singers { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

    }
}