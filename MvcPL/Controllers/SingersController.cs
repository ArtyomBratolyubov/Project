using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Helpers;

namespace MvcPL.Controllers
{
    public class SingersController : _BaseController
    {

        public SingersController(ISingerService singerService, IUserService userService,
            ISongService songService, IAlbumService albumService,
            ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService,commentSongService)
        {

        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            SingersViewModel svm = new SingersViewModel();

            svm.SingersCount = singerService.GetSingerCount();

            svm.Pages = Helper.GetCountOfPages(svm.SingersCount);

            if (page == null)
                page = 1;

            svm.Singers = singerService.GetSingersOnPage(Helper.pageSize, (int)page).Select(m => m.ToMvcSinger());

            InitializeBaseModel(svm);

            return View(svm);
        }


        public ActionResult Page(int? page)
        {
            if (page == null)
                page = 1;

            InitViewBag();

            IEnumerable<SingerModel> sm = singerService.GetSingersOnPage(Helper.pageSize, (int)page).Select(m => m.ToMvcSinger());

            if (Request.IsAjaxRequest())
                return PartialView(sm);

            SingersViewModel svm = new SingersViewModel();

            svm.SingersCount = singerService.GetSingerCount();

            svm.Pages = Helper.GetCountOfPages(svm.SingersCount);

            svm.Singers = sm;

            svm.Page = (int)page;

            InitializeBaseModel(svm);

            return View("Index", svm);
        }


    }
}
