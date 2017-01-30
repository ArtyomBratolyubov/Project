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
    public class GenreController : _BaseController
    {
        private IGenreService genreService;

        public GenreController(IUserService userService, IGenreService genreService,
            ISongService songService, IAlbumService albumService,
            ISingerService singerService, ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService,commentSongService)
        {
            this.genreService = genreService;
        }



        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            GenreEditViewModel sevm = new GenreEditViewModel();

            InitializeBaseModel(sevm);

            return View(sevm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreEditViewModel sevm)
        {
            InitializeBaseModel(sevm);

            if (ModelState.IsValid)
            {
                genreService.Create(sevm.Genre.ToBllGenre(sevm.UserId));

                return RedirectToAction("Index", "Genres");
            }

            return View(sevm);
        }


        [Authorize]
        public ActionResult Delete(int? Id)
        {
            try
            {
                GenreEditViewModel sevm = new GenreEditViewModel();

                sevm.Genre = genreService.GetEntity((int)Id).ToMvcGenre();

                InitializeBaseModel(sevm);

                if (!CheckAutorized(sevm.Genre.AuthorId, sevm.Roles))
                    return RedirectToAction("Error");

                return View(sevm);

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
                genreService.Delete((int)Id);

                return RedirectToAction("Index", "Genres");
            }


            return View();
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {
            try
            {
                GenreEditViewModel sevm = new GenreEditViewModel();

                sevm.Genre = genreService.GetEntity((int)Id).ToMvcGenre();

                InitializeBaseModel(sevm);

                if (!CheckAutorized(sevm.Genre.AuthorId, sevm.Roles))
                    return RedirectToAction("Error");

                return View(sevm);
            }
            catch (ArgumentNullException ex)
            {
                return RedirectToAction("Index", "Error", new { msg = "Информация не найдена" });
            }

        }

        [HttpPost, ActionName("Edit")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(GenreEditViewModel sevm, int? Id)
        {
            if (ModelState.IsValid)
            {
                var Genre = genreService.GetEntity((int)Id);

                Genre.Name = sevm.Genre.Name;

                Genre.Description = sevm.Genre.Description;

                genreService.Update(Genre);

                return RedirectToAction("Index", "Genres");
            }


            return View();
        }
    }
}