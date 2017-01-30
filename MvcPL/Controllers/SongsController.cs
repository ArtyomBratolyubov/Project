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
    public class SongsController : _BaseController
    {

        private IGenreService genreService;
        private IRateSongService rateSongService;

        public SongsController(IAlbumService albumService, IUserService userService,
            ISingerService singerService, IGenreService genreService,
            ISongService songService, IRateSongService rateSongService,
            ICommentSongService commentSongService)
            : base(userService,songService,singerService,albumService,commentSongService)
        {

            this.genreService = genreService;

            this.rateSongService = rateSongService;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            SongsViewModel avm = new SongsViewModel();

            avm.SongsCount = songService.GetSongCount();

            avm.Pages = Helper.GetCountOfPages(avm.SongsCount);

            if (page == null)
                page = 1;

            avm.Songs = songService.GetSongsOnPage(Helper.pageSize, (int)page)
                .Select(m => m.ToMvcSong())
                .Select(m =>
                {
                    var album = albumService.GetEntity(m.AlbumId);

                    m.SingerId = album.SingerId;

                    m.AlbumId = album.Id;

                    m.GenreId = album.GenreId;

                    m.AlbumName = album.Name;

                    m.GenreName = genreService.GetEntity(album.GenreId).Name;

                    m.SingerName = singerService.GetEntity(album.SingerId).Name;

                    m.Rating = rateSongService.GetRatingBySongId(m.Id);

                    return m;
                }).OrderBy(m => m.SingerName).ThenBy(m => m.Name);

            InitializeBaseModel(avm);

            return View(avm);

        }


        public ActionResult Page(int? page)
        {
            if (page == null)
                page = 1;

            InitViewBag();

            IEnumerable<SongModel> sm = songService.GetSongsOnPage(Helper.pageSize, (int)page)
                .Select(m => m.ToMvcSong())
                .Select(m =>
                {
                    var album = albumService.GetEntity(m.AlbumId);

                    m.SingerId = album.SingerId;

                    m.AlbumId = album.Id;

                    m.GenreId = album.GenreId;

                    m.AlbumName = album.Name;

                    m.GenreName = genreService.GetEntity(album.GenreId).Name;

                    m.SingerName = singerService.GetEntity(album.SingerId).Name;

                    m.Rating = rateSongService.GetRatingBySongId(m.Id);

                    return m;
                }).OrderBy(m => m.SingerName).ThenBy(m => m.Name);

            if (Request.IsAjaxRequest())
                return PartialView(sm);

            SongsViewModel avm = new SongsViewModel();

            avm.SongsCount = songService.GetSongCount();

            avm.Pages = Helper.GetCountOfPages(avm.SongsCount);

            if (page == null)
                page = 1;

            avm.Songs = sm;

           

            InitializeBaseModel(avm);

            return View("Index", avm);
        }


    }
}
