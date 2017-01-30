using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Web;

namespace BLL.Interface.Services
{
    public interface ISongService : IService<BLLSong>
    {
        int GetSongCount();

        IEnumerable<BLLSong> GetSongsOnPage(int pageSize, int page);

        void Create(BLLSong Song, HttpPostedFileBase file);

        void Delete(int Id);

        void Update(BLLSong e, HttpPostedFileBase file);

        IEnumerable<BLLSong> GetMostRatedSongs(int count);

        IEnumerable<BLLSong> GetMostCommentedSongs(int count);
    }
}


