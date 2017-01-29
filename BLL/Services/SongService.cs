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
    public class SongService : ISongService
    {
        private readonly IUnitOfWork uow;

        public SongService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLSong GetEntity(int id)
        {
            return uow.SongRepository.GetById(id).ToBllSong();
        }

        public void Delete(BLLSong Song)
        {
            uow.SongRepository.Delete(Song.ToDalSong());
            uow.Commit();
        }

        public void Delete(int Id)
        {
            int? imgId = uow.SongRepository.GetById(Id).MusicId;
            if (imgId >= 1)
                uow.FileRepository.Delete(uow.FileRepository.GetById((int)imgId));

            uow.SongRepository.Delete(uow.SongRepository.GetById(Id));
            uow.Commit();
        }

        public IEnumerable<BLLSong> GetAllEntities()
        {
            return uow.SongRepository.GetAll().Select(Song => Song.ToBllSong());
        }

        public void Create(BLLSong Song)
        {


            uow.SongRepository.Create(Song.ToDalSong());
            uow.Commit();
        }

        public void Create(BLLSong Song, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                var File = new BLLFile()
                {
                    MimeType = file.ContentType,
                    Data = new byte[file.ContentLength]
                };
                file.InputStream.Read(File.Data, 0, file.ContentLength);

                Song.MusicId = uow.FileRepository.Create(File.ToDalFile());
            }

            uow.SongRepository.Create(Song.ToDalSong());
            uow.Commit();
        }

        public void Update(BLLSong e)
        {
            uow.SongRepository.Update(e.ToDalSong());
            uow.Commit();
        }

        public void Update(BLLSong e, HttpPostedFileBase file)
        {
            if (file == null)
            {
                Update(e);
                return;
            }
            if (e.MusicId >= 1)
                uow.FileRepository.Delete(uow.FileRepository.GetById((int)e.MusicId));

            var File = new BLLFile()
            {
                MimeType = file.ContentType,
                Data = new byte[file.ContentLength]
            };
            file.InputStream.Read(File.Data, 0, file.ContentLength);

            e.MusicId = uow.FileRepository.Create(File.ToDalFile());

            uow.SongRepository.Update(e.ToDalSong());
            uow.Commit();
        }

        public int GetSongCount()
        {
            return uow.SongRepository.GetAll().Count();
        }

        public IEnumerable<BLLSong> GetSongsOnPage(int pageSize = 10, int page = 1)
        {
            return uow.SongRepository.GetAll()
                .Skip((page - 1) * pageSize)
                 .Take(pageSize).Select(Song => Song.ToBllSong())
                   .OrderBy(Song => Song.Name)
                     ;

        }

        #endregion
    }
}
