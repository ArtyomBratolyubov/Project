using System;
namespace BLL.Interface.Entities
{
    public class BLLAlbum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int? ImageId { get; set; }

        public int GenreId { get; set; }

        public int SingerId { get; set; }

        public DateTime? DateOut { get; set; }
    }
}


