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
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork uow;

        public AlbumService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLAlbum GetEntity(int id)
        {
            return uow.AlbumRepository.GetById(id).ToBllAlbum();
        }

        public void Delete(BLLAlbum Album)
        {
            uow.AlbumRepository.Delete(Album.ToDalAlbum());
            uow.Commit();
        }

        public void Delete(int Id)
        {
            int? imgId = uow.AlbumRepository.GetById(Id).ImageId;
            if (imgId >= 1)
                uow.FileRepository.Delete(uow.FileRepository.GetById((int)imgId));

            uow.AlbumRepository.Delete(uow.AlbumRepository.GetById(Id));
            uow.Commit();
        }

        public IEnumerable<BLLAlbum> GetAllEntities()
        {
            return uow.AlbumRepository.GetAll().Select(Album => Album.ToBllAlbum());
        }

        public void Create(BLLAlbum Album)
        {


            uow.AlbumRepository.Create(Album.ToDalAlbum());
            uow.Commit();
        }

        public void Create(BLLAlbum Album, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                var File = new BLLFile()
                {
                    MimeType = file.ContentType,
                    Data = new byte[file.ContentLength]
                };
                file.InputStream.Read(File.Data, 0, file.ContentLength);

                Album.ImageId = uow.FileRepository.Create(File.ToDalFile());
            }

            uow.AlbumRepository.Create(Album.ToDalAlbum());
            uow.Commit();
        }

        public void Update(BLLAlbum e)
        {
            uow.AlbumRepository.Update(e.ToDalAlbum());
            uow.Commit();
        }

        public void Update(BLLAlbum e, HttpPostedFileBase file)
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

            uow.AlbumRepository.Update(e.ToDalAlbum());
            uow.Commit();
        }

        public int GetAlbumCount()
        {
            return uow.AlbumRepository.GetAll().Count();
        }

        public IEnumerable<BLLAlbum> GetAlbumsOnPage(int pageSize = 10, int page = 1)
        {
            return uow.AlbumRepository.GetAll()
                .OrderBy(Album => Album.Name)
                .Skip((page - 1) * pageSize)
                 .Take(pageSize).Select(Album => Album.ToBllAlbum());

        }

        public IEnumerable<BLLAlbum> GetAlbumsBySinger(int singerId)
        {
            return GetAllEntities().Where(m => m.SingerId == singerId);
        }

        #endregion

    }
}
