﻿using System;

namespace DAL.Interface.DTO
{
    public class DalCommentSong : IEntity
    {
        public int Id { get; set; }

        public int SongId { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}