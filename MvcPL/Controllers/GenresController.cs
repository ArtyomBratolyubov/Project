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
    public class GenresController : _BaseController
    {
        IGenreService GenreService;

        public GenresController(IGenreService GenreService, IUserService userService)
            : base(userService)
        {
            this.GenreService = GenreService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            GenresViewModel svm = new GenresViewModel();


            svm.Genres = GenreService.GetAllEntities().OrderBy(m => m.Name).Select(m => m.ToMvcGenre());

            InitializeBaseModel(svm);

            return View(svm);
        }



    }
}
