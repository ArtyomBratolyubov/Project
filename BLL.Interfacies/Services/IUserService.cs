using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        BLLUser GetUserEntity(int id);
        IEnumerable<BLLUser> GetAllUserEntities();
        void RegistrateUser(BLLUser user);
        bool LoginUser(BLLUser user);     
      
    }
}


