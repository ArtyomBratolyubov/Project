using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this BLLUser userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.UserName,
                RoleId = userEntity.RoleId
            };
        }
        public static BLLUser ToBllUser(this DalUser dalUser)
        {
            return new BLLUser()
            {
                Id = dalUser.Id,
                UserName = dalUser.Name,
                RoleId = dalUser.RoleId
            };
        }


        public static DalGenre ToDalGenre(this BLLGenre userEntity)
        {
            return new DalGenre()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Description = userEntity.Description,
                AuthorId = userEntity.AuthorId
            };
        }
        public static BLLGenre ToBllGenre(this DalGenre dalUser)
        {
            return new BLLGenre()
            {
                Id = dalUser.Id,
                Name = dalUser.Name,
                Description = dalUser.Description,
                AuthorId = dalUser.AuthorId
            };
        }


        public static DalSinger ToDalSinger(this BLLSinger Entity)
        {
            return new DalSinger()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Description = Entity.Description,
                AuthorId = Entity.AuthorId,
                ImageId = Entity.ImageId
            };
        }
        public static BLLSinger ToBllSinger(this DalSinger dal)
        {
            return new BLLSinger()
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId,
                ImageId = dal.ImageId
            };
        }


        public static BLLFile ToBLLFile(this DalFile dalFile)
        {
            var ormFile = new BLLFile()
            {
                Id = dalFile.Id,
                Data = dalFile.Data,
                MimeType = dalFile.MimeType
            };
            return ormFile;
        }
        public static DalFile ToDalFile(this BLLFile ormFile)
        {
            if (ormFile == null)
                return null;
            return new DalFile()
            {
                Id = ormFile.Id,
                Data = ormFile.Data,
                MimeType = ormFile.MimeType,
            };
        }


        public static DalAlbum ToDalAlbum(this BLLAlbum Entity)
        {
            return new DalAlbum()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Description = Entity.Description,
                AuthorId = Entity.AuthorId,
                ImageId = Entity.ImageId,
                SingerId = Entity.SingerId,
                GenreId = Entity.GenreId,
                DateOut = Entity.DateOut
            };
        }
        public static BLLAlbum ToBllAlbum(this DalAlbum dal)
        {
            return new BLLAlbum()
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


        public static DalSong ToDalSong(this BLLSong Entity)
        {
            return new DalSong()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Description = Entity.Description,
                AuthorId = Entity.AuthorId,
                MusicId = Entity.MusicId,
                DateAdded = Entity.DateAdded,
                AlbumId = Entity.AlbumId
            };
        }
        public static BLLSong ToBllSong(this DalSong dal)
        {
            return new BLLSong()
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                AuthorId = dal.AuthorId,
                MusicId = dal.MusicId,
                DateAdded = dal.DateAdded,
                AlbumId = dal.AlbumId
            };
        }


        public static DalRateSong ToDalRateSong(this BLLRateSong Entity)
        {
            return new DalRateSong()
            {
                Id = Entity.Id,
                UserId = Entity.UserId,
                SongId = Entity.SongId,
                Rate = Entity.Rate
            };
        }
        public static BLLRateSong ToBllRateSong(this DalRateSong dal)
        {
            return new BLLRateSong()
            {
                Id = dal.Id,
                UserId = dal.UserId,
                SongId = dal.SongId,
                Rate = dal.Rate
            };
        }


        public static DalCommentSong ToDalCommentSong(this BLLCommentSong bll)
        {
            if (bll == null)
                throw new ArgumentNullException("bll");

            var dal = new DalCommentSong
            {
                Id = bll.Id,
                UserId = bll.UserId,
                SongId = bll.SongId,
                Text = bll.Text,
                DateAdded = bll.DateAdded
            };

            return dal;
        }
        public static BLLCommentSong ToBllCommentSong(this DalCommentSong dal)
        {
            return new BLLCommentSong
            {
                Id = dal.Id,
                UserId = dal.UserId,
                SongId = dal.SongId,
                Text = dal.Text,
                DateAdded = dal.DateAdded
            };
        }
    }
}
