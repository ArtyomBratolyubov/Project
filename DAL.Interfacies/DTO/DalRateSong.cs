

namespace DAL.Interface.DTO
{
    public class DalRateSong : IEntity
    {
        public int Id { get; set; }

        public int SongId { get; set; }

        public int UserId { get; set; }

        public int Rate { get; set; }
    }
}