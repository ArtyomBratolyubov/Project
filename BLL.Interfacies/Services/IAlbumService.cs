using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Web;

namespace BLL.Interface.Services
{
    public interface IAlbumService : IService<BLLAlbum>
    {
        int GetAlbumCount();

        IEnumerable<BLLAlbum> GetAlbumsOnPage(int pageSize, int page);

        void Create(BLLAlbum Album, HttpPostedFileBase file);

        void Delete(int Id);

        void Update(BLLAlbum e, HttpPostedFileBase file);

        IEnumerable<BLLAlbum> GetAlbumsBySinger(int singerId);
    }
}


