using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{

    public class HomeController : _BaseController
    {

        IGenreService genreService;
        IRateSongService rateSongService;

        public HomeController(IUserService userService, ISongService songService,
            IAlbumService albumService, IGenreService genreService,
            ISingerService singerService, IRateSongService rateSongService,
            ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService,commentSongService)
        {

            this.genreService = genreService;
            this.rateSongService = rateSongService;
        }

        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            ivm.LastAddedSongs = songService.GetAllEntities()
                .OrderByDescending(m => m.DateAdded)
                .Select(m => m.ToMvcSong())
                .Select(m =>
                {
                    var album = albumService.GetEntity(m.AlbumId);
                    m.AlbumName = album.Name;
                    m.SingerId = album.SingerId;
                    m.SingerName = singerService.GetEntity(album.SingerId).Name;
                    m.GenreName = genreService.GetEntity(album.GenreId).Name;
                    m.GenreId = album.GenreId;
                    m.Rating = rateSongService.GetRatingBySongId(m.Id);

                    return m;
                }).Take(15);

            InitializeBaseModel(ivm);

            return View(ivm);
        }

    }
}