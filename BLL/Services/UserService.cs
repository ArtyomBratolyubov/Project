using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using WebMatrix.WebData;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        #region Public Methods

        public BLLUser GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public IEnumerable<BLLUser> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        #endregion



        public void RegistrateUser(BLLUser user)
        {
            WebSecurity.CreateUserAndAccount(user.UserName, user.Password);
            var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;

            if (roles.RoleExists("User"))
                roles.AddUsersToRoles(new[] { user.UserName }, new[] { "User" });

            WebSecurity.Login(user.UserName, user.Password);


        }

        public bool LoginUser(BLLUser user)
        {
            return WebSecurity.Login(user.UserName, user.Password);
        }
    }
}
