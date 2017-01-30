using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Web;

namespace BLL.Interface.Services
{
    public interface ICommentSongService : IService<BLLCommentSong>
    {

        void Delete(int Id);
    }
}


