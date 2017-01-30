using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using WebMatrix.WebData;

namespace MvcPL.Controllers
{
    [Authorize]
    public class UsersController : _BaseController
    {

        public UsersController(IUserService userService, ISongService songService,
            IAlbumService albumService, ISingerService singerService,
            ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService, commentSongService)
        {

        }

        [Authorize]
        public ActionResult Index()
        {

            UsersViewModel uvm = new UsersViewModel();

            Initialize(uvm);

            InitializeBaseModel(uvm);

            return View(uvm);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult MakeAdmin(int? userId)
        {
            UsersViewModel uvm = new UsersViewModel();

            UserService.MakeAdmin(UserService.GetUserEntity((int)userId));

            Initialize(uvm);

            InitializeBaseModel(uvm);

            return View("Index", uvm);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult MakeModer(int? userId)
        {
            UsersViewModel uvm = new UsersViewModel();

            UserService.MakeModer(UserService.GetUserEntity((int)userId));

            Initialize(uvm);

            InitializeBaseModel(uvm);

            return View("Index", uvm);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult MakeUser(int? userId)
        {
            UsersViewModel uvm = new UsersViewModel();

            UserService.MakeUser(UserService.GetUserEntity((int)userId));

            Initialize(uvm);

            InitializeBaseModel(uvm);

            return View("Index", uvm);

        }

        private void Initialize(UsersViewModel uvm)
        {
            var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;

            uvm.Users = UserService.GetAllUserEntities()
                .Select(m => m.ToMvcUser())
                .OrderBy(m => m.UserName)
                .Select(m =>
                {
                    m.Roles = roles.GetRolesForUser(m.UserName);
                    return m;
                });
        }
    }
}
