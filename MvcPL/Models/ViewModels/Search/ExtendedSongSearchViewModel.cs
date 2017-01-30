using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class ExtendedSongSearchViewModel : BaseViewModel
    {
        public ExtendedSongSearchModel SearchModel { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

        public IEnumerable<SongModel> Songs { get; set; }
    }
}