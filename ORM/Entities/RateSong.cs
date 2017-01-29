namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RateSong
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("Song")]
        public int? SongId { get; set; }
        [Required]
        public virtual Song Song { get; set; }


        [Required]
        [Range(0, 5)]
        public int Mark { get; set; }

    }
}




