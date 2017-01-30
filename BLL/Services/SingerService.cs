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
    public class SingerService : ISingerService
    {
        private readonly IUnitOfWork uow;

        public SingerService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLSinger GetEntity(int id)
        {
            return uow.SingerRepository.GetById(id).ToBllSinger();
        }

        public void Delete(BLLSinger Singer)
        {
            uow.SingerRepository.Delete(Singer.ToDalSinger());
            uow.Commit();
        }

        public void Delete(int Id)
        {
            int? imgId = uow.SingerRepository.GetById(Id).ImageId;
            if (imgId >= 1)
                uow.FileRepository.Delete(uow.FileRepository.GetById((int)imgId));

            uow.SingerRepository.Delete(uow.SingerRepository.GetById(Id));
            uow.Commit();
        }

        public IEnumerable<BLLSinger> GetAllEntities()
        {
            return uow.SingerRepository.GetAll().Select(Singer => Singer.ToBllSinger());
        }

        public void Create(BLLSinger Singer)
        {


            uow.SingerRepository.Create(Singer.ToDalSinger());
            uow.Commit();
        }

        public void Create(BLLSinger Singer, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                var File = new BLLFile()
                {
                    MimeType = file.ContentType,
                    Data = new byte[file.ContentLength]
                };
                file.InputStream.Read(File.Data, 0, file.ContentLength);

                Singer.ImageId = uow.FileRepository.Create(File.ToDalFile());
            }

            uow.SingerRepository.Create(Singer.ToDalSinger());
            uow.Commit();
        }

        public void Update(BLLSinger e)
        {
            uow.SingerRepository.Update(e.ToDalSinger());
            uow.Commit();
        }

        public void Update(BLLSinger e, HttpPostedFileBase file)
        {
            if (file == null)
            {
                Update(e);
                return;
            }
            if (e.ImageId >= 1)
                uow.FileRepository.Delete(uow.FileRepository.GetById((int)e.ImageId));

            var File = new BLLFile()
            {
                MimeType = file.ContentType,
                Data = new byte[file.ContentLength]
            };
            file.InputStream.Read(File.Data, 0, file.ContentLength);

            e.ImageId = uow.FileRepository.Create(File.ToDalFile());

            uow.SingerRepository.Update(e.ToDalSinger());
            uow.Commit();
        }

        public int GetSingerCount()
        {
            return uow.SingerRepository.GetAll().Count();
        }

        public IEnumerable<BLLSinger> GetSingersOnPage(int pageSize = 10, int page = 1)
        {
            return uow.SingerRepository.GetAll()
                .OrderBy(Singer => Singer.Name)
                .Skip((page - 1) * pageSize)
                 .Take(pageSize).Select(Singer => Singer.ToBllSinger());

        }

        #endregion



    }
}
