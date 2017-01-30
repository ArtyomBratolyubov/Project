using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{

    public class SongEditViewModel : BaseViewModel
    {
        public SongEditModel Song { get; set; }

        public IEnumerable<AlbumModel> Albums { get; set; }

        public IEnumerable<SelectListItem> AlbumsOptions { get; set; }

        public IEnumerable<SingerModel> Singers { get; set; }

    }
}