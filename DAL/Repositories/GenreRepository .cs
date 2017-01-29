using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using DAL.Interfacies.DTO;

namespace DAL.Concrete
{
    public sealed class GenreRepository : IGenreRepository
    {
        DbContext context;

        public GenreRepository(DbContext db)
        {
            this.context = db;
        }

        public IEnumerable<DalGenre> GetAll()
        {
            return context.Set<Genre>().Select(Genre => new DalGenre()
                        {
                            Id = Genre.Id,
                            Name = Genre.Name,
                            Description = Genre.Description,
                            AuthorId = Genre.AuthorId ?? 0
                        });
        }

        public DalGenre GetById(int key)
        {
            var ormGenre = context.Set<Genre>().FirstOrDefault(Genre => Genre.Id == key);
            return ormGenre.ToDalGenre();
        }

        public DalGenre GetByPredicate(Expression<Func<DalGenre, bool>> f)
        {
            //Expression<Func<DalGenre, bool>> -> Expression<Func<Genre, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalGenre e)
        {
            var Genre = e.ToOrmGenre();
            context.Set<Genre>().Add(Genre);

            return Genre.Id;
        }

        public void Delete(DalGenre e)
        {
            var Genre = e.ToOrmGenre();

            Genre = context.Set<Genre>().Single(u => u.Id == Genre.Id);

            context.Set<Genre>().Remove(Genre);
        }

        public void Update(DalGenre e)
        {
            var genre = context.Set<Genre>().First(r => r.Id == e.Id);

            if (genre != null)
            {
                genre.Name = e.Name;
                genre.Description = e.Description;


                context.Entry(genre).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}