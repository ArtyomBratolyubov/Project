using BLL.Interface.Entities;
using MvcPL.Models;
using System.Web;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserModel ToMvcUser(this BLLUser userEntity)
        {
            return new UserModel()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                //Role = (Role)userEntity.RoleId
            };
        }
        public static BLLUser ToBllUser(this UserModel userViewModel)
        {
            return new BLLUser()
            {
                Id = userViewModel.Id,
                UserName = userViewModel.UserName,
                //RoleId = (int)userViewModel.Role
            };
        }


        public static GenreModel ToMvcGenre(this BLLGenre BLLGenre)
        {
            return new GenreModel()
            {
                Id = BLLGenre.Id,
                Name = BLLGenre.Name,
                Description = BLLGenre.Description,
                AuthorId = BLLGenre.AuthorId
            };
        }
        public static BLLGenre ToBllGenre(this GenreModel genreModel)
        {
            return new BLLGenre()
            {
                Id = genreModel.Id,
                Name = genreModel.Name,
                Description = genreModel.Description,
                AuthorId = genreModel.AuthorId
            };
        }
        public static BLLGenre ToBllGenre(this GenreModel genreModel, int Author)
        {
            return new BLLGenre()
            {
                Id = genreModel.Id,
                Name = genreModel.Name,
                Description = genreModel.Description,
                AuthorId = Author
            };
        }


        public static SingerModel ToMvcSinger(this BLLSinger Entity)
        {
            return new SingerModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Description = Entity.Description,
                AuthorId = Entity.AuthorId,
                ImageId = Entity.ImageId
            };
        }
        public static BLLSinger ToBllSinger(this SingerModel Model)
        {
            return new BLLSinger()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Model.AuthorId,
                ImageId = Model.ImageId
            };
        }
        public static BLLSinger ToBllSinger(this SingerModel Model, int Author)
        {
            return new BLLSinger()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Author,
                ImageId = Model.ImageId
            };
        }


        public static BLLUser ToBllUser(this LoginModel Model)
        {
            return new BLLUser()
            {
                UserName = Model.UserName,
                Password = Model.Password,
                RoleId = (int)Model.RoleId
            };
        }
        public static BLLUser ToBllUser(this RegistrateModel Model)
        {
            return new BLLUser()
            {
                UserName = Model.UserName,
                Password = Model.Password,
                RoleId = (int)Model.RoleId
            };
        }


        public static AlbumModel ToMvcAlbum(this BLLAlbum Entity)
        {
            return new AlbumModel()
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
        public static BLLAlbum ToBllAlbum(this AlbumEditModel Model)
        {
            return new BLLAlbum()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Model.AuthorId,
                ImageId = Model.ImageId,
                SingerId = Model.SingerId,
                GenreId = Model.GenreId,
                DateOut = Model.DateOut
            };
        }
        public static BLLAlbum ToBllAlbum(this AlbumEditModel Model, int Author)
        {
            return new BLLAlbum()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Author,
                ImageId = Model.ImageId,
                SingerId = Model.SingerId,
                GenreId = Model.GenreId,
                DateOut = Model.DateOut
            };
        }


        public static SongModel ToMvcSong(this BLLSong Entity)
        {
            return new SongModel()
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
        public static BLLSong ToBllSong(this SongEditModel Model)
        {
            return new BLLSong()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Model.AuthorId,
                MusicId = Model.MusicId,
                DateAdded = Model.DateAdded,
                AlbumId = Model.AlbumId
            };
        }
        public static BLLSong ToBllSong(this SongEditModel Model, int Author)
        {
            return new BLLSong()
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                AuthorId = Author,
                MusicId = Model.MusicId,
                DateAdded = Model.DateAdded,
                AlbumId = Model.AlbumId
            };
        }

        public static RateSongModel ToMvcRateSong(this BLLRateSong Entity)
        {
            return new RateSongModel()
            {
                Id = Entity.Id,
                UserId = Entity.UserId,
                SongId = Entity.SongId,
                Rate = Entity.Rate
            };
        }
        public static BLLRateSong ToBllRateSong(this RateSongModel dal)
        {
            return new BLLRateSong()
            {
                Id = dal.Id,
                UserId = dal.UserId,
                SongId = dal.SongId,
                Rate = dal.Rate
            };
        }

        public static CommentSongModel ToMvcCommentSong(this BLLCommentSong Entity)
        {
            return new CommentSongModel()
            {
                Id = Entity.Id,
                UserId = Entity.UserId,
                SongId = Entity.SongId,
                DateAdded = Entity.DateAdded,
                Text = Entity.Text
            };
        }
        public static BLLCommentSong ToBllCommentSong(this CommentSongModel dal)
        {
            return new BLLCommentSong()
            {
                Id = dal.Id,
                UserId = dal.UserId,
                SongId = dal.SongId,
                DateAdded = dal.DateAdded,
                Text = dal.Text
            };
        }
    }
}