using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class AlbumsViewModel : BaseViewModel
    {
        public IEnumerable<AlbumModel> Albums { get; set; }

        public int AlbumsCount { get; set; }

        public int Pages { get; set; }

    }
}