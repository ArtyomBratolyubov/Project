﻿using System;
namespace BLL.Interface.Entities
{
    public class BLLCommentSong
    {
        public int Id { get; set; }

        public int SongId { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}


