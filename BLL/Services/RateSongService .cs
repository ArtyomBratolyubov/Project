using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System.Web;

namespace BLL.Services
{
    public class RateSongService : IRateSongService
    {
        private readonly IUnitOfWork uow;

        public RateSongService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLRateSong GetEntity(int id)
        {
            return uow.RateSongRepository.GetById(id).ToBllRateSong();
        }

        public void Delete(BLLRateSong RateSong)
        {
            uow.RateSongRepository.Delete(RateSong.ToDalRateSong());
            uow.Commit();
        }

        public void Delete(int Id)
        {
            uow.RateSongRepository.Delete(uow.RateSongRepository.GetById(Id));
            uow.Commit();
        }

        public IEnumerable<BLLRateSong> GetAllEntities()
        {
            return uow.RateSongRepository.GetAll().Select(RateSong => RateSong.ToBllRateSong());
        }

        public void Create(BLLRateSong RateSong)
        {
            if (uow.RateSongRepository.
                GetAll().
                Where(m => (m.SongId == RateSong.SongId && m.UserId == RateSong.UserId))
                .Count() == 0)
                uow.RateSongRepository.Create(RateSong.ToDalRateSong());
            else
                uow.RateSongRepository.Update(RateSong.ToDalRateSong());
            uow.Commit();
        }


        public void Update(BLLRateSong e)
        {
            uow.RateSongRepository.Update(e.ToDalRateSong());
            uow.Commit();
        }


        public int GetRateSongCount()
        {
            return uow.RateSongRepository.GetAll().Count();
        }



        #endregion


        public double GetRatingBySongId(int Id)
        {
            var e = uow.RateSongRepository.GetAll()
                .Where(m => m.SongId == Id);

            if (e.Count() != 0)
            {
                return e.Average(m => m.Rate);
            }
            else
                return 0.0;
        }

        public int GetRateCountBySongId(int Id)
        {
            return uow.RateSongRepository.GetAll()
                .Where(m => m.SongId == Id)
                .Count();
        }
    }
}
