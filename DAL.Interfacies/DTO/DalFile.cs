

namespace DAL.Interface.DTO
{
    public class DalFile : IEntity
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string MimeType { get; set; }

    }
}