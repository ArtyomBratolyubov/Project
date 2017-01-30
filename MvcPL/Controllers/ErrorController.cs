using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Models;
using BLL.Interface.Services;

namespace MvcPL.Controllers
{
    public class ErrorController : _BaseController
    {
        public ErrorController(IUserService userService,
            ISongService songService, IAlbumService albumService,
            ISingerService singerService, ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService, commentSongService)
        {
        }


        public ActionResult Index()
        {
            BaseViewModel bvm = new BaseViewModel();

            InitializeBaseModel(bvm);

            InitViewBag();

            return View("Index", bvm);
        }

    }
}
