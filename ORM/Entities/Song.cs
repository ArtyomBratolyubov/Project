namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }


        [ForeignKey("Music")]
        public int? MusicId { get; set; }
        public virtual File Music { get; set; }


        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
        [Required]
        public virtual Album Album { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? DateAdded { get; set; }
    }
}



