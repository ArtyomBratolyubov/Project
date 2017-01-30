using System;
namespace BLL.Interface.Entities
{
    public class BLLRateSong
    {
        public int Id { get; set; }

        public int SongId { get; set; }

        public int UserId { get; set; }

        public int Rate { get; set; }
    }
}


