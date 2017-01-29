using System;
namespace DAL.Interface.DTO
{
    public class DalSong : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int AlbumId { get; set; }

        public int? MusicId { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}