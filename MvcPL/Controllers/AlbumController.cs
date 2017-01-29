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
    public class AlbumController : _BaseController
    {
        private IAlbumService albumService;
        private ISingerService singerService;
        private IGenreService genreService;
        private ISongService songService;
        private IRateSongService rateSongService;

        public AlbumController(IAlbumService albumService, IUserService userService,
            ISingerService singerService, IGenreService genreService,
            ISongService songService, IRateSongService rateSongService)
            : base(userService)
        {
            this.albumService = albumService;
            this.singerService = singerService;
            this.genreService = genreService;
            this.songService = songService;
            this.rateSongService = rateSongService;
        }

        [HttpGet]
        public ActionResult Index(int? Id)
        {
            AlbumViewModel avm = new AlbumViewModel();

            avm.Album = albumService.GetEntity((int)Id).ToMvcAlbum();

            avm.Songs = songService.GetAllEntities()
                .Where(m => m.AlbumId == avm.Album.Id)
                .Select(m => m.ToMvcSong())
                .Select(m =>
                {
                    if (m != null)
                        m.Rating = rateSongService.GetRatingBySongId(m.Id);
                    return m;
                }); ;

            avm.Album.GenreName = genreService.GetEntity(avm.Album.GenreId).Name;

            avm.Album.SingerName = singerService.GetEntity(avm.Album.SingerId).Name;

            InitializeBaseModel(avm);

            return View(avm);

        }


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            AlbumEditViewModel aevm = new AlbumEditViewModel();

            aevm.Singers = singerService.GetAllEntities().Select(m => m.ToMvcSinger()).OrderBy(m => m.Name);

            aevm.Genres = genreService.GetAllEntities().Select(m => m.ToMvcGenre()).OrderBy(m => m.Name);

            InitializeBaseModel(aevm);

            return View(aevm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumEditViewModel aevm, HttpPostedFileBase File = null)
        {
            InitializeBaseModel(aevm);

            if (ModelState.IsValid)
            {
                albumService.Create(aevm.Album.ToBllAlbum(aevm.UserId), File);

                return RedirectToAction("Index", "Albums");
            }

            aevm.Singers = singerService.GetAllEntities().Select(m => m.ToMvcSinger()).OrderBy(m => m.Name);

            aevm.Genres = genreService.GetAllEntities().Select(m => m.ToMvcGenre()).OrderBy(m => m.Name);

            return View(aevm);
        }


        [Authorize]
        public ActionResult Delete(int? Id)
        {
            try
            {
                AlbumViewModel aevm = new AlbumViewModel();

                aevm.Album = albumService.GetEntity((int)Id).ToMvcAlbum();

                aevm.Album.GenreName = genreService.GetEntity(aevm.Album.GenreId).Name;

                aevm.Album.SingerName = singerService.GetEntity(aevm.Album.SingerId).Name;

                InitializeBaseModel(aevm);

                if (!CheckAutorized(aevm.Album.AuthorId, aevm.Roles))
                    return RedirectToAction("Error");

                return View(aevm);

            }
            catch (ArgumentNullException ex)
            {
                return RedirectToAction("Index", "Error", new { msg = "Информация не найдена" });
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? Id)
        {

            if (ModelState.IsValid && Id >= 1)
            {
                albumService.Delete((int)Id);

                return RedirectToAction("Index", "albums");
            }


            return View();
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {
            try
            {
                AlbumEditViewModel aevm = new AlbumEditViewModel();

                aevm.Album = albumService.GetEntity((int)Id).ToMvcAlbum();

                aevm.Singers = singerService.GetAllEntities().Select(m => m.ToMvcSinger()).OrderBy(m => m.Name);

                aevm.Genres = genreService.GetAllEntities().Select(m => m.ToMvcGenre()).OrderBy(m => m.Name);

                InitializeBaseModel(aevm);

                if (!CheckAutorized(aevm.Album.AuthorId, aevm.Roles))
                    return RedirectToAction("Error");

                return View(aevm);
            }
            catch (ArgumentNullException ex)
            {
                return RedirectToAction("Index", "Error", new { msg = "Информация не найдена" });
            }

        }

        [HttpPost, ActionName("Edit")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(AlbumEditViewModel aevm, int? Id, HttpPostedFileBase File = null)
        {
            if (ModelState.IsValid)
            {
                var album = albumService.GetEntity((int)Id);

                album.Name = aevm.Album.Name;

                album.Description = aevm.Album.Description;

                album.DateOut = aevm.Album.DateOut;

                album.SingerId = aevm.Album.SingerId;

                album.GenreId = aevm.Album.GenreId;

                albumService.Update(album, File);

                return RedirectToAction("Index", "albums");
            }


            return View();
        }


    }
}
