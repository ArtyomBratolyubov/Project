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
    public class AlbumsController : _BaseController
    {
        private IGenreService genreService;

        public AlbumsController(IAlbumService albumService, IUserService userService,
            ISingerService singerService, IGenreService genreService,
            ISongService songService, ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService, commentSongService)
        {

            this.genreService = genreService;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            AlbumsViewModel avm = new AlbumsViewModel();

            avm.AlbumsCount = albumService.GetAlbumCount();

            avm.Pages = Helper.GetCountOfPages(avm.AlbumsCount);

            if (page == null)
                page = 1;

            avm.Albums = albumService.GetAlbumsOnPage(Helper.pageSize, (int)page)
                .Select(m => m.ToMvcAlbum())
                .Select(m =>
                {
                    m.GenreName = genreService.GetEntity(m.GenreId).Name;
                    m.SingerName = singerService.GetEntity(m.SingerId).Name;
                    return m;
                });

            InitializeBaseModel(avm);

            return View(avm);

        }


        public ActionResult Page(int? page)
        {
            if (page == null)
                page = 1;

            InitViewBag();

            IEnumerable<AlbumModel> sm = albumService.GetAlbumsOnPage(Helper.pageSize, (int)page)
                .Select(m => m.ToMvcAlbum())
                .Select(m =>
                {
                    m.GenreName = genreService.GetEntity(m.GenreId).Name;
                    m.SingerName = singerService.GetEntity(m.SingerId).Name;
                    return m;
                });

            if (Request.IsAjaxRequest())
                return PartialView(sm);

            AlbumsViewModel avm = new AlbumsViewModel();

            avm.AlbumsCount = albumService.GetAlbumCount();

            avm.Pages = Helper.GetCountOfPages(avm.AlbumsCount);

            if (page == null)
                page = 1;

            avm.Albums = sm;

            foreach (var i in avm.Albums)
            {
                i.GenreName = genreService.GetEntity(i.GenreId).Name;

                i.SingerName = singerService.GetEntity(i.SingerId).Name;
            }

            InitializeBaseModel(avm);

            return View("Index", avm);
        }


    }
}
