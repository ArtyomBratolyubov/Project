using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;


namespace DAL.Interfacies.DTO
{
    static class DalMapper
    {

        public static DalGenre ToDalGenre(this Genre orm)
        {
            if (orm == null)
                throw new ArgumentNullException("orm");

            var dal = new DalGenre
            {
                Id = orm.Id,
                Name = orm.Name,
                Description = orm.Description,
                AuthorId = orm.AuthorId ?? 0
            };

            return dal;
        }
        public static Genre ToOrmGenre(this DalGenre dal)
        {
            return new Genre
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId
            };
        }

        public static DalSinger ToDalSinger(this Singer orm)
        {
            if (orm == null)
                throw new ArgumentNullException("orm");

            var dal = new DalSinger
            {
                Id = orm.Id,
                Name = orm.Name,
                Description = orm.Description,
                AuthorId = orm.AuthorId ?? 0,
                ImageId = orm.ImageId
            };

            return dal;
        }
        public static Singer ToOrmSinger(this DalSinger dal)
        {
            return new Singer
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId,
                ImageId = dal.ImageId
            };
        }


        public static File ToOrmFile(this DalFile dalImage)
        {
            var ormImage = new File()
            {
                Id = dalImage.Id,
                Data = dalImage.Data,
                MimeType = dalImage.MimeType,

            };
            return ormImage;
        }
        public static DalFile ToDalFile(this File ormImage)
        {
            return new DalFile()
            {
                Id = ormImage.Id,
                Data = ormImage.Data,
                MimeType = ormImage.MimeType,
            };
        }


        public static DalAlbum ToDalAlbum(this Album orm)
        {
            if (orm == null)
                throw new ArgumentNullException("orm");

            var dal = new DalAlbum
            {
                Id = orm.Id,
                Name = orm.Name,
                Description = orm.Description,
                AuthorId = orm.AuthorId ?? 0,
                ImageId = orm.ImageId,
                SingerId = orm.SingerId ?? 0,
                GenreId = orm.GenreId ?? 0,
                DateOut = orm.DateOut
            };

            return dal;
        }
        public static Album ToOrmAlbum(this DalAlbum dal)
        {
            return new Album
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId,
                ImageId = dal.ImageId,
                SingerId = dal.SingerId,
                GenreId = dal.GenreId,
                DateOut = dal.DateOut
            };
        }


        public static DalSong ToDalSong(this Song orm)
        {
            if (orm == null)
                throw new ArgumentNullException("orm");

            var dal = new DalSong
            {
                Id = orm.Id,
                Name = orm.Name,
                Description = orm.Description,
                AuthorId = orm.AuthorId ?? 0,
                MusicId = orm.MusicId,
                AlbumId = orm.AlbumId ?? 0,
                DateAdded = orm.DateAdded
            };

            return dal;
        }
        public static Song ToOrmSong(this DalSong dal)
        {
            return new Song
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId,
                MusicId = dal.MusicId,
                AlbumId = dal.AlbumId,
                DateAdded = dal.DateAdded
            };
        }


        public static DalRateSong ToDalRateSong(this RateSong orm)
        {
            if (orm == null)
                throw new ArgumentNullException("orm");

            var dal = new DalRateSong
            {
                Id = orm.Id,
                UserId = orm.UserId ?? 0,
                SongId = orm.SongId ?? 0,
                Rate = orm.Mark
            };

            return dal;
        }
        public static RateSong ToOrmRateSong(this DalRateSong dal)
        {
            return new RateSong
            {
                Id = dal.Id,
                UserId = dal.UserId,
                SongId = dal.SongId,
                Mark = dal.Rate
            };
        }
    }
}
