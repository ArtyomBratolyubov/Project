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
    public class RateSongRepository : IRateSongRepository
    {
        private readonly DbContext context;

        public RateSongRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalRateSong> GetAll()
        {
            //return context.Set<RateSong>().Select(RateSong => RateSong.ToDalRateSong());
            return context.Set<RateSong>().Select(RateSong => new DalRateSong
            {
                Id = RateSong.Id,
                UserId = RateSong.UserId ?? 0,
                SongId = RateSong.SongId ?? 0,
                Rate = RateSong.Mark
            });
        }


        public DalRateSong GetById(int key)
        {
            var ormRateSong = context.Set<RateSong>().FirstOrDefault(RateSong => RateSong.Id == key);
            return ormRateSong.ToDalRateSong();
        }

        public DalRateSong GetByPredicate(Expression<Func<DalRateSong, bool>> f)
        {
            //Expression<Func<DalRateSong, bool>> -> Expression<Func<RateSong, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalRateSong e)
        {
            var RateSong = e.ToOrmRateSong();

            RateSong.User = context.Set<User>().FirstOrDefault(m => m.Id == RateSong.UserId);
            RateSong.Song = context.Set<Song>().FirstOrDefault(m => m.Id == RateSong.SongId);

            context.Set<RateSong>().Add(RateSong);

            return RateSong.Id;
        }

        public void Delete(DalRateSong e)
        {
            var RateSong = context.Set<RateSong>().Single(u => u.Id == e.Id);

            context.Set<RateSong>().Remove(RateSong);
        }

        public void Update(DalRateSong e)
        {
            var RateSong = context.Set<RateSong>().First(r => (r.UserId == e.UserId && r.SongId == e.SongId));

            if (RateSong != null)
            {
                RateSong.Mark = e.Rate;

                RateSong.Song = context.Set<Song>().First(r => (r.Id == e.SongId));

                context.Entry(RateSong).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}