using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Web;

namespace BLL.Interface.Services
{
    public interface IRateSongService : IService<BLLRateSong>
    {
        double GetRatingBySongId(int Id);

        int GetRateCountBySongId(int Id);
    }
}


