using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SingerViewModel : BaseViewModel
    {
        public SingerModel Singer { get; set; }

        public IEnumerable<AlbumModel> Albums { get; set; }

    }
}