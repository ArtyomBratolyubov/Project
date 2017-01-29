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
    public class SingerController : _BaseController
    {
        private ISingerService singerService;
        private IAlbumService albumService;
        private IGenreService genreService;

        public SingerController(ISingerService singerService, IUserService userService,
            IAlbumService albumService, IGenreService genreService)
            : base(userService)
        {
            this.singerService = singerService;
            this.albumService = albumService;
            this.genreService = genreService;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id < 1)
                return RedirectToAction("Index", "Error");

            SingerViewModel svm = new SingerViewModel();

            svm.Singer = singerService.GetEntity((int)id).ToMvcSinger();

            svm.Albums = albumService.GetAlbumsBySinger((int)id)
                .Select(m => m.ToMvcAlbum())
                .Select(m =>
                {
                    m.GenreName = genreService.GetEntity(m.GenreId).Name;
                    return m;
                });

            InitializeBaseModel(svm);

            return View(svm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            SingerEditViewModel sevm = new SingerEditViewModel();

            InitializeBaseModel(sevm);

            return View(sevm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SingerEditViewModel sevm, HttpPostedFileBase File = null)
        {
            InitializeBaseModel(sevm);

            if (ModelState.IsValid)
            {
                singerService.Create(sevm.Singer.ToBllSinger(sevm.UserId), File);

                return RedirectToAction("Index", "Singers");
            }

            return View(sevm);
        }


        [Authorize]
        public ActionResult Delete(int? Id)
        {
            try
            {
                SingerEditViewModel sevm = new SingerEditViewModel();

                sevm.Singer = singerService.GetEntity((int)Id).ToMvcSinger();

                InitializeBaseModel(sevm);

                if (!CheckAutorized(sevm.Singer.AuthorId, sevm.Roles))
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
                singerService.Delete((int)Id);

                return RedirectToAction("Index", "Singers");
            }


            return View();
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {
            try
            {
                SingerEditViewModel sevm = new SingerEditViewModel();

                sevm.Singer = singerService.GetEntity((int)Id).ToMvcSinger();

                InitializeBaseModel(sevm);

                if (!CheckAutorized(sevm.Singer.AuthorId, sevm.Roles))
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
        public ActionResult EditConfirmed(SingerEditViewModel sevm, int? Id, HttpPostedFileBase File = null)
        {
            if (ModelState.IsValid)
            {
                var singer = singerService.GetEntity((int)Id);

                singer.Name = sevm.Singer.Name;

                singer.Description = sevm.Singer.Description;

                singerService.Update(singer, File);

                return RedirectToAction("Index", "Singers");
            }


            return View();
        }
    }
}