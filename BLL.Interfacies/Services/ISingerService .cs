using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Web;

namespace BLL.Interface.Services
{
    public interface ISingerService : IService<BLLSinger>
    {
        int GetSingerCount();

        IEnumerable<BLLSinger> GetSingersOnPage(int pageSize, int page);

        void Create(BLLSinger Singer, HttpPostedFileBase file);

        void Delete(int Id);

        void Update(BLLSinger e, HttpPostedFileBase file);
    }
}


