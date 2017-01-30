using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using System.Collections.Generic;
using System;

namespace MvcPL.Controllers
{

    public class SearchController : _BaseController
    {

        IGenreService genreService;
        IRateSongService rateSongService;

        public SearchController(IUserService userService, ISongService songService,
            IAlbumService albumService, IGenreService genreService,
            ISingerService singerService, IRateSongService rateSongService,
            ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService, commentSongService)
        {

            this.genreService = genreService;
            this.rateSongService = rateSongService;
        }

        

        public ActionResult Simple(string q, SimpleSearchViewModel model)
        {
            if (model == null)
                model = new SimpleSearchViewModel();

            string msg;

            if (!string.IsNullOrEmpty(model.Text))
                msg = model.Text;
            else
                if (q != null)
                {
                    msg = q;
                    model.Text = q;
                }
                else
                    msg = "";

            if (!string.IsNullOrEmpty(msg))
            {
                model.Albums = albumService.GetAllEntities()
                    .Select(m => m.ToMvcAlbum())
                    .Where(m => m.Name.Contains(msg, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(m => m.Name)
                    .Select(m =>
                    {
                        m.SingerName = singerService.GetEntity(m.SingerId).Name;
                        m.GenreName = genreService.GetEntity(m.GenreId).Name;
                        return m;
                    });

                model.Singers = singerService.GetAllEntities()
                    .Select(m => m.ToMvcSinger())
                    .Where(m => m.Name.Contains(msg, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(m => m.Name);

                model.Songs = songService.GetAllEntities()
                    .Select(m => m.ToMvcSong())
                    .Where(m => m.Name.Contains(msg, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(m => m.Name)
                    .Select(m =>
                    {
                        var album = albumService.GetEntity(m.AlbumId);
                        m.SingerName = singerService.GetEntity(album.SingerId).Name;
                        m.SingerId = album.SingerId;
                        m.GenreName = genreService.GetEntity(album.GenreId).Name;
                        m.GenreId = album.GenreId;
                        m.Rating = rateSongService.GetRatingBySongId(m.Id);
                        m.AlbumName = album.Name;

                        return m;
                    });
            }
            else
            {
                model.Albums = Enumerable.Empty<AlbumModel>();

                model.Singers = Enumerable.Empty<SingerModel>();

                model.Songs = Enumerable.Empty<SongModel>();
            }

            InitializeBaseModel(model);

            if (Request.IsAjaxRequest())
                return PartialView("Resaults", model);

            return View(model);
        }

        public ActionResult ExtendedSong()
        {
            var model = new ExtendedSongSearchViewModel();

            model.Genres = genreService.GetAllEntities().Select(m => m.ToMvcGenre());

            InitializeBaseModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult SongResaults(ExtendedSongSearchViewModel model)
        {
            model.Genres = genreService.GetAllEntities().Select(m => m.ToMvcGenre());

            model.Songs = songService.GetAllEntities()
                .Select(m => m.ToMvcSong())
                .Select(m =>
                {
                    m.Rating = rateSongService.GetRatingBySongId(m.Id);

                    var album = albumService.GetEntity(m.AlbumId);

                    m.GenreId = album.GenreId;

                    m.DateAdded = album.DateOut;

                    m.SingerId = album.SingerId;

                    m.AlbumName = album.Name;

                    m.GenreName = genreService.GetEntity(album.GenreId).Name;

                    m.SingerName = singerService.GetEntity(album.SingerId).Name;

                    return m;
                });

            if (model.SearchModel.Name != null)
                model.Songs = model.Songs.Where(m => m.Name.Contains(model.SearchModel.Name, StringComparison.OrdinalIgnoreCase));

            if (model.SearchModel.HasMusic)
                model.Songs = model.Songs.Where(m => m.MusicId >= 1);

            if (model.SearchModel.DateFrom != null)
                model.Songs = model.Songs.Where(m => m.DateAdded >= model.SearchModel.DateFrom);

            if (model.SearchModel.DateTo != null)
                model.Songs = model.Songs.Where(m => m.DateAdded <= model.SearchModel.DateTo);

            if (model.SearchModel.RateFrom != null)
                model.Songs = model.Songs.Where(m => m.Rating >= model.SearchModel.RateFrom);

            if (model.SearchModel.RateTo != null)
                model.Songs = model.Songs.Where(m => m.Rating <= model.SearchModel.RateTo);

            if (model.SearchModel.SelectedGenres != null && model.SearchModel.SelectedGenres.Count() != 0)
                model.Songs = model.Songs.Where(m => model.SearchModel.SelectedGenres
                    .Contains(m.GenreId));

            InitializeBaseModel(model);

            if (Request.IsAjaxRequest())
                return PartialView(model);



            return View("ExtendedSong", model);
        }

        
    }

    public static class StringExt
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}