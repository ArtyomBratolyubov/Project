namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Singer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }


        public virtual ICollection<Album> Albums { get; set; }



        [ForeignKey("Author")]
        public int? AuthorId { get; set; }


        [ForeignKey("Image")]
        public int? ImageId { get; set; }

        public virtual File Image { get; set; }

        public virtual User Author { get; set; }

        public Singer()
        {
            Albums = new HashSet<Album>();
        }

    }
}



