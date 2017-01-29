namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RateAlbum
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }


        [Required]
        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }


        [Required]
        [Range(0, 5)]
        public int Mark { get; set; }

    }
}




