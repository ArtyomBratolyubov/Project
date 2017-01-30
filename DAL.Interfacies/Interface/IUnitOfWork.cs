using DAL.Interface.DTO;
using System;
using System.Data.Entity;


namespace DAL.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get;  }

        IRepository<DalGenre> GenreRepository { get; }

        IRepository<DalSinger> SingerRepository { get; }

        IRepository<DalFile> FileRepository { get; }

        IRepository<DalAlbum> AlbumRepository { get; }

        IRepository<DalSong> SongRepository { get; }

        IRepository<DalRateSong> RateSongRepository { get; }

        IRepository<DalCommentSong> CommentSongRepository { get; }

        void Commit();

    }
}