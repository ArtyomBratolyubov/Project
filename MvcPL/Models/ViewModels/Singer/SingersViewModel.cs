using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SingersViewModel : BaseViewModel
    {
        public IEnumerable<SingerModel> Singers { get; set; }

        public int SingersCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

    }
}