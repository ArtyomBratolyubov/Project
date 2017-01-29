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
    public class SongController : _BaseController
    {
        private IAlbumService albumService;
        private ISingerService singerService;
        private IGenreService genreService;
        private ISongService songService;
        private IRateSongService rateSongService;

        public SongController(IAlbumService albumService, IUserService userService,
            ISingerService singerService, ISongService songService,
            IGenreService genreService, IRateSongService rateSongService)
            : base(userService)
        {
            this.albumService = albumService;
            this.singerService = singerService;
            this.songService = songService;
            this.genreService = genreService;
            this.rateSongService = rateSongService;
        }

        [HttpGet]
        public ActionResult Index(int? Id)
        {
            if (Id < 1)
                return RedirectToAction("Index", "Error");

            SongViewModel aevm = new SongViewModel();

            aevm.Song = songService.GetEntity((int)Id).ToMvcSong();

            AlbumModel album = albumService.GetEntity(aevm.Song.AlbumId).ToMvcAlbum();

            aevm.Song.AlbumName = album.Name;

            aevm.Song.GenreName = genreService.GetEntity(album.GenreId).Name;

            aevm.Song.SingerName = singerService.GetEntity(album.SingerId).Name;

            aevm.Song.AlbumId = album.Id;

            aevm.Song.SingerId = album.SingerId;

            aevm.Song.GenreId = album.GenreId;


            aevm.Song.Rating = rateSongService.GetRatingBySongId(aevm.Song.Id);

            InitializeBaseModel(aevm);

            return View(aevm);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            SongEditViewModel aevm = new SongEditViewModel();


            aevm.Albums = albumService.GetAllEntities()
                                     .Select(m => m.ToMvcAlbum())
                                     .OrderBy(m => m.Name);

            InitializeBaseModel(aevm);

            return View(aevm);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongEditViewModel aevm, HttpPostedFileBase File = null)
        {
            InitializeBaseModel(aevm);

            if (ModelState.IsValid)
            {
                aevm.Song.DateAdded = DateTime.Now;

                songService.Create(aevm.Song.ToBllSong(aevm.UserId), File);

                return RedirectToAction("Index", "Songs");
            }

            aevm.Singers = singerService.GetAllEntities().Select(m => m.ToMvcSinger()).OrderBy(m => m.Name);

            aevm.Albums = albumService.GetAllEntities()
                .Select(m => m.ToMvcAlbum())
                .OrderBy(m => m.Name);

            return View(aevm);
        }


        [Authorize]
        public ActionResult Delete(int? Id)
        {
            try
            {
                SongViewModel aevm = new SongViewModel();

                aevm.Song = songService.GetEntity((int)Id).ToMvcSong();

                AlbumModel album = albumService.GetEntity(aevm.Song.AlbumId).ToMvcAlbum();

                aevm.Song.AlbumName = album.Name;

                aevm.Song.GenreName = genreService.GetEntity(album.GenreId).Name;

                aevm.Song.SingerName = singerService.GetEntity(album.SingerId).Name;

                aevm.Song.AlbumId = album.Id;

                aevm.Song.SingerId = album.SingerId;

                aevm.Song.GenreId = album.GenreId;

                InitializeBaseModel(aevm);

                if (!CheckAutorized(aevm.Song.AuthorId, aevm.Roles))
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
                songService.Delete((int)Id);

                return RedirectToAction("Index", "Songs");
            }


            return View();
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {
            try
            {
                SongEditViewModel aevm = new SongEditViewModel();

                aevm.Song = songService.GetEntity((int)Id).ToMvcSong();

                AlbumModel album = albumService.GetEntity(aevm.Song.AlbumId).ToMvcAlbum();

                aevm.Albums = albumService.GetAllEntities()
                                                     .Select(m => m.ToMvcAlbum())
                                                     .OrderBy(m => m.Name);


                InitializeBaseModel(aevm);

                if (!CheckAutorized(aevm.Song.AuthorId, aevm.Roles))
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
        public ActionResult EditConfirmed(SongEditViewModel aevm, int? Id, HttpPostedFileBase File = null)
        {
            if (ModelState.IsValid)
            {
                var song = songService.GetEntity((int)Id);

                song.Name = aevm.Song.Name;

                song.Description = aevm.Song.Description;

                song.AlbumId = aevm.Song.AlbumId;

                songService.Update(song, File);

                return RedirectToAction("Index", "Songs");
            }


            return View();
        }

        [Authorize]
        public ActionResult Rate(int? rate, int? songId)
        {
            var songViewModel = new SongEditViewModel();

            InitializeBaseModel(songViewModel);

            var rating = new RateSongModel
            {
                UserId = songViewModel.UserId,
                SongId = (int)songId,
                Rate = (int)rate
            };

            rateSongService.Create(rating.ToBllSong());

            var songModel = new SongModel();

            songModel.Rating = rateSongService.GetRatingBySongId((int)songId);

            songModel.Id = (int)songId;

            if (Request.IsAjaxRequest())
                return PartialView(songModel);

            SongViewModel aevm = new SongViewModel();

            aevm.Song = songService.GetEntity((int)songId).ToMvcSong();

            AlbumModel album = albumService.GetEntity(aevm.Song.AlbumId).ToMvcAlbum();

            aevm.Song.AlbumName = album.Name;

            aevm.Song.GenreName = genreService.GetEntity(album.GenreId).Name;

            aevm.Song.SingerName = singerService.GetEntity(album.SingerId).Name;

            aevm.Song.AlbumId = album.Id;

            aevm.Song.SingerId = album.SingerId;

            aevm.Song.GenreId = album.GenreId;


            aevm.Song.Rating = rateSongService.GetRatingBySongId(aevm.Song.Id);

            InitializeBaseModel(aevm);

            return View("Index", aevm);

        }
    }
}
