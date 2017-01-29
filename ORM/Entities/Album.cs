namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime? DateOut { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }


        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }


        public virtual ICollection<Song> Songs { get; set; }


        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }


        [ForeignKey("Image")]
        public int? ImageId { get; set; }
        public virtual File Image { get; set; }

        [ForeignKey("Singer")]
        public int? SingerId { get; set; }
        [Required]
        public virtual Singer Singer { get; set; }


        public Album()
        {
            Songs = new HashSet<Song>();
        }
    }
}



