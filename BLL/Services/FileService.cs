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
    public class FileService : IFileService
    {
        private readonly IUnitOfWork uow;

        public FileService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        #region Public Methods

        public BLLFile GetEntity(int id)
        {
            return uow.FileRepository.GetById(id).ToBLLFile();
        }

        public void Delete(BLLFile File)
        {
            uow.FileRepository.Delete(File.ToDalFile());
            uow.Commit();
        }

        public IEnumerable<BLLFile> GetAllEntities()
        {
            return uow.FileRepository.GetAll().Select(File => File.ToBLLFile());
        }

        public void Create(BLLFile File)
        {

            uow.FileRepository.Create(File.ToDalFile());
            uow.Commit();
        }


        public void Update(BLLFile e)
        {
            throw new System.NotImplementedException();
        }

        

        #endregion



       
    }
}
