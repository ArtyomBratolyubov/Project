namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}



