using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using ORM;

namespace DAL.Concrete
{
    public class SongRepository : ISongRepository
    {
        private readonly DbContext context;

        public SongRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalSong> GetAll()
        {
            //return context.Set<Song>().Select(Song => Song.ToDalSong());
            return context.Set<Song>().Select(Song => new DalSong
            {
                Id = Song.Id,
                Name = Song.Name,
                Description = Song.Description,
                AuthorId = Song.AuthorId ?? 0,
                MusicId = Song.MusicId,
                AlbumId = Song.AlbumId ?? 0,
                DateAdded = Song.DateAdded
            });
        }


        public DalSong GetById(int key)
        {
            var ormSong = context.Set<Song>().FirstOrDefault(Song => Song.Id == key);
            return ormSong.ToDalSong();
        }

        public DalSong GetByPredicate(Expression<Func<DalSong, bool>> f)
        {
            //Expression<Func<DalSong, bool>> -> Expression<Func<Song, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalSong e)
        {
            var Song = e.ToOrmSong();

            Song.Album = context.Set<Album>().FirstOrDefault(m => m.Id == Song.AlbumId);

            context.Set<Song>().Add(Song);

            return Song.Id;
        }

        public void Delete(DalSong e)
        {
            var Song = context.Set<Song>().Single(u => u.Id == e.Id);

            context.Set<Song>().Remove(Song);
        }

        public void Update(DalSong e)
        {
            var Song = context.Set<Song>().First(r => r.Id == e.Id);

            if (Song != null)
            {
                Song.Name = e.Name;
                Song.Description = e.Description;
                Song.AlbumId = e.AlbumId;
                Song.MusicId = e.MusicId;
                Song.Album = context.Set<Album>().First(r => r.Id == e.AlbumId);

                context.Entry(Song).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}