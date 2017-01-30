using System;
using System.Data.Entity;
using System.Diagnostics;
using DAL.Interface.Repository;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using DAL.Interface.DTO;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public DbContext Context { get; private set; }


        IRepository<DalGenre> genreRepository;

        IRepository<DalSinger> singerRepository;

        IRepository<DalFile> fileRepository;

        IRepository<DalAlbum> albumRepository;

        IRepository<DalSong> songRepository;

        IRepository<DalRateSong> rateSongRepository;

        IRepository<DalCommentSong> commentSongRepository;


        public IRepository<DalGenre> GenreRepository
        {
            get 
            {
                if (this.genreRepository == null)
                {
                    this.genreRepository = new DAL.Concrete.GenreRepository(Context);
                }
                return genreRepository;
            }
        }

        public IRepository<DalSinger> SingerRepository
        {
            get
            {
                if (this.singerRepository == null)
                {
                    this.singerRepository = new DAL.Concrete.SingerRepository(Context);
                }
                return singerRepository;
            }
        }

        public IRepository<DalFile> FileRepository
        {
            get
            {
                if (this.fileRepository == null)
                {
                    this.fileRepository = new DAL.Concrete.FileRepository(Context);
                }
                return fileRepository;
            }
        }

        public IRepository<DalAlbum> AlbumRepository
        {
            get
            {
                if (this.albumRepository == null)
                {
                    this.albumRepository = new DAL.Concrete.AlbumRepository(Context);
                }
                return albumRepository;
            }
        }

        public IRepository<DalSong> SongRepository
        {
            get
            {
                if (this.songRepository == null)
                {
                    this.songRepository = new DAL.Concrete.SongRepository(Context);
                }
                return songRepository;
            }
        }

        public IRepository<DalRateSong> RateSongRepository
        {
            get
            {
                if (this.rateSongRepository == null)
                {
                    this.rateSongRepository = new DAL.Concrete.RateSongRepository(Context);
                }
                return rateSongRepository;
            }
        }        

        public IRepository<DalCommentSong> CommentSongRepository 
             {
            get
            {
                if (this.commentSongRepository == null)
                {
                    this.commentSongRepository = new DAL.Concrete.CommentSongRepository(Context);
                }
                return commentSongRepository;
            }
        }     

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }


        public void Commit()
        {
            try
            {
                if (Context != null)
                {
                    Context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        
    }
}