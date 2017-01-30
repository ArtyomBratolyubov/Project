using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        BLLUser GetUserEntity(int Id);
        IEnumerable<BLLUser> GetAllUserEntities();
        void RegistrateUser(BLLUser user);
        bool LoginUser(BLLUser user);    
 
        void MakeAdmin(BLLUser user);
      
        void MakeModer(BLLUser user);

        void MakeUser(BLLUser user);
    }
}


