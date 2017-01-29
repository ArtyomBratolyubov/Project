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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbContext context;

        public AlbumRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalAlbum> GetAll()
        {
            //return context.Set<Album>().Select(Album => Album.ToDalAlbum());
            return context.Set<Album>().Select(Album => new DalAlbum
            {
                Id = Album.Id,
                Name = Album.Name,
                Description = Album.Description,
                AuthorId = Album.Author.Id,
                ImageId = Album.ImageId,
                SingerId = Album.SingerId ?? 0,
                GenreId = Album.GenreId ?? 0,
                DateOut = Album.DateOut
            });
        }


        public DalAlbum GetById(int key)
        {
            var ormAlbum = context.Set<Album>().FirstOrDefault(Album => Album.Id == key);
            return ormAlbum.ToDalAlbum();
        }

        public DalAlbum GetByPredicate(Expression<Func<DalAlbum, bool>> f)
        {
            //Expression<Func<DalAlbum, bool>> -> Expression<Func<Album, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalAlbum e)
        {
            var Album = e.ToOrmAlbum();
            Album.Singer = context.Set<Singer>().FirstOrDefault(m => m.Id == Album.SingerId);
            Album.Genre = context.Set<Genre>().FirstOrDefault(m => m.Id == Album.GenreId);
            context.Set<Album>().Add(Album);

            return Album.Id;
        }

        public void Delete(DalAlbum e)
        {

            var Album = context.Set<Album>().Single(u => u.Id == e.Id);

            context.Set<Album>().Remove(Album);

        }

        public void Update(DalAlbum e)
        {
            var Album = context.Set<Album>().First(r => r.Id == e.Id);

            if (Album != null)
            {
                Album.Name = e.Name;
                Album.Description = e.Description;
                Album.ImageId = e.ImageId;
                Album.SingerId = e.SingerId;
                Album.GenreId = e.GenreId;
                Album.DateOut = e.DateOut;
                Album.Singer = context.Set<Singer>().First(r => r.Id == e.SingerId);
                Album.Genre = context.Set<Genre>().First(r => r.Id == e.GenreId);

                context.Entry(Album).State = System.Data.Entity.EntityState.Modified;
            }
        }


    }
}