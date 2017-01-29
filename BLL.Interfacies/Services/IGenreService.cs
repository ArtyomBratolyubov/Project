using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IGenreService: IService<BLLGenre>
    {
        void Delete(int Id);
    }
}


