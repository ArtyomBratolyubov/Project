﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class SongsViewModel : BaseViewModel
    {
        public IEnumerable<SongModel> Songs { get; set; }

        public int SongsCount { get; set; }

        public int Pages { get; set; }

    }
}