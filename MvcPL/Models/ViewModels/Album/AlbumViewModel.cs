using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class AlbumViewModel : BaseViewModel
    {
        public AlbumModel Album { get; set; }

        public IEnumerable<SongModel> Songs { get; set; }


    }
}