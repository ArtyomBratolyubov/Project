using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class AlbumModel : AlbumEditModel
    {

        public string SingerName { get; set; }

        public string GenreName { get; set; }

    }
}