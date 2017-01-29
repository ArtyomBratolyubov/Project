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
        ISongService songService;
        IAlbumService albumService;
        IGenreService genreService;
        ISingerService singerService;

        public HomeController(IUserService userService, ISongService songService,
            IAlbumService albumService, IGenreService genreService, ISingerService singerService)
            : base(userService)
        {
            this.songService = songService;
            this.genreService = genreService;
            this.albumService = albumService;
            this.singerService = singerService;
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

                    return m;
                }).Take(15);

            InitializeBaseModel(ivm);

            return View(ivm);
        }

    }
}