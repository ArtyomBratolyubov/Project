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
    public class CommentSongService : ICommentSongService
    {
        private readonly IUnitOfWork uow;

        public CommentSongService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLCommentSong GetEntity(int id)
        {
            return uow.CommentSongRepository.GetById(id).ToBllCommentSong();
        }

        public void Delete(BLLCommentSong CommentSong)
        {
            uow.CommentSongRepository.Delete(CommentSong.ToDalCommentSong());
            uow.Commit();
        }

        public void Delete(int Id)
        {
            uow.CommentSongRepository.Delete(uow.CommentSongRepository.GetById(Id));
            uow.Commit();
        }

        public IEnumerable<BLLCommentSong> GetAllEntities()
        {
            return uow.CommentSongRepository.GetAll().Select(CommentSong => CommentSong.ToBllCommentSong());
        }

        public void Create(BLLCommentSong CommentSong)
        {


            uow.CommentSongRepository.Create(CommentSong.ToDalCommentSong());
            uow.Commit();
        }



        public void Update(BLLCommentSong e)
        {
            uow.CommentSongRepository.Update(e.ToDalCommentSong());
            uow.Commit();
        }


        #endregion



    }
}
