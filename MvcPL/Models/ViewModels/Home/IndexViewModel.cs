﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{

    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<SongModel> LastAddedSongs { get; set; }

    }
}