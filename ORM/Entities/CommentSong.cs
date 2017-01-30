namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentSong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("Song")]
        public int? SongId { get; set; }
        [Required]
        public virtual Song Song { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? DateAdded { get; set; }
    }
}




