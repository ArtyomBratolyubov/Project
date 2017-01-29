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
    public class SingerRepository : ISingerRepository
    {
        private readonly DbContext context;

        public SingerRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalSinger> GetAll()
        {
            //return context.Set<Singer>().Select(Singer => Singer.ToDalSinger());
            return context.Set<Singer>().Select(Singer => new DalSinger
            {
                Id = Singer.Id,
                Name = Singer.Name,
                Description = Singer.Description,
                AuthorId = Singer.Author.Id,
                ImageId = Singer.ImageId
            });
        }


        public DalSinger GetById(int key)
        {
            var ormSinger = context.Set<Singer>().FirstOrDefault(Singer => Singer.Id == key);
            return ormSinger.ToDalSinger();
        }

        public DalSinger GetByPredicate(Expression<Func<DalSinger, bool>> f)
        {
            //Expression<Func<DalSinger, bool>> -> Expression<Func<Singer, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalSinger e)
        {
            var Singer = e.ToOrmSinger();
            context.Set<Singer>().Add(Singer);

            return Singer.Id;
        }

        public void Delete(DalSinger e)
        {
            var Singer = e.ToOrmSinger();
            Singer = context.Set<Singer>().Single(u => u.Id == Singer.Id);

            //context.Set<Album>().RemoveRange(context.Set<Album>().Where(i => i.SingerId == Singer.Id));

            context.Set<Singer>().Remove(Singer);
        }

        public void Update(DalSinger e)
        {
            var singer = context.Set<Singer>().First(r => r.Id == e.Id);

            if (singer != null)
            {
                singer.Name = e.Name;
                singer.Description = e.Description;
                singer.ImageId = e.ImageId;

                context.Entry(singer).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}