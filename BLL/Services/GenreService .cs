using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork uow;
        private readonly IGenreRepository GenreRepository;
        public GenreService(IUnitOfWork uow, IGenreRepository repository)
        {
            this.uow = uow;
            this.GenreRepository = repository;
        }


        #region Public Methods

        public BLLGenre GetEntity(int id)
        {
            return GenreRepository.GetById(id).ToBllGenre();
        }
        public void Delete(BLLGenre Genre)
        {
            GenreRepository.Delete(Genre.ToDalGenre());
            uow.Commit();
        }
        public void Delete(int Id)
        {
            uow.GenreRepository.Delete(uow.GenreRepository.GetById(Id));
            uow.Commit();
        }
        public IEnumerable<BLLGenre> GetAllEntities()
        {
            return GenreRepository.GetAll().Select(Genre => Genre.ToBllGenre());
        }
        public void Create(BLLGenre Genre)
        {
            GenreRepository.Create(Genre.ToDalGenre());
            uow.Commit();
        }

        public void Update(BLLGenre e)
        {
            uow.GenreRepository.Update(e.ToDalGenre());
            uow.Commit();
        }
        #endregion

    }
}
