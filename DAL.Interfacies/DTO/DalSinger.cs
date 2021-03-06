﻿namespace DAL.Interface.DTO
{
    public class DalSinger : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int? ImageId { get; set; }
    }
}