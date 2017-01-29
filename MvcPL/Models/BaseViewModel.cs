using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class BaseViewModel
    {
        public IEnumerable<SongEditModel> TopRatedSongs { get; set; }

        public int UserId { get; set; }

        public string[] Roles { get; set; }

    }
}