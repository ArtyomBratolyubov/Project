using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using WebMatrix.WebData;
using MvcPL.Filters;


namespace MvcPL.Controllers
{
    [InitializeSimpleMembership]
    public class _BaseController : Controller
    {
        public IUserService UserService { get; private set; }

        public ISongService songService { get; private set; }

        public ISingerService singerService { get; private set; }

        public IAlbumService albumService { get; private set; }

        public ICommentSongService commentSongService { get; private set; }

        public _BaseController()
        {

        }

        public _BaseController(IUserService userService, ISongService songService,
            ISingerService singerService, IAlbumService albumService,
            ICommentSongService commentSongService)
        {
            this.UserService = userService;
            this.songService = songService;
            this.singerService = singerService;
            this.albumService = albumService;
            this.commentSongService = commentSongService;
        }

        public void InitializeBaseModel(BaseViewModel bvm)
        {
            if (bvm == null)
                throw new ArgumentNullException("bvm");


            bvm.UserId = WebSecurity.CurrentUserId;


            var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
            if (!string.IsNullOrEmpty(WebSecurity.CurrentUserName))
                bvm.Roles = roles.GetRolesForUser(WebSecurity.CurrentUserName);

            bvm.TopRatedSongs = songService.GetMostRatedSongs(5)
                          .Select(m => m.ToMvcSong())
                          .Select(m =>
                          {
                              var album = albumService.GetEntity(m.AlbumId);
                              m.SingerName = singerService.GetEntity(album.SingerId).Name;
                              return m;
                          });

            bvm.TopCommentedSongs = songService.GetMostCommentedSongs(5)
                          .Select(m => m.ToMvcSong())
                          .Select(m =>
                          {
                              var album = albumService.GetEntity(m.AlbumId);
                              m.SingerName = singerService.GetEntity(album.SingerId).Name;
                              return m;
                          });

            ViewBag.Roles = bvm.Roles;
            ViewBag.UserId = bvm.UserId;
        }

        public void InitViewBag()
        {
            ViewBag.UserId = WebSecurity.CurrentUserId;

            var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
            if (!string.IsNullOrEmpty(WebSecurity.CurrentUserName))
                ViewBag.Roles = roles.GetRolesForUser(WebSecurity.CurrentUserName);
        }

        public bool CheckAutorized(int authorId, string[] roles)
        {
            return (WebSecurity.CurrentUserId == authorId || roles.Contains("Moderator") || roles.Contains("Admin"));
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            RedirectToAction("Index", "Error");
        }

    }
}
