using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();

            kernel.Bind<ISingerService>().To<SingerService>();
            kernel.Bind<ISingerRepository>().To<SingerRepository>();

            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IFileRepository>().To<FileRepository>();

            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<IAlbumRepository>().To<AlbumRepository>();

            kernel.Bind<ISongService>().To<SongService>();
            kernel.Bind<ISongRepository>().To<SongRepository>();

            kernel.Bind<IRateSongRepository>().To<RateSongRepository>();
            kernel.Bind<IRateSongService>().To<RateSongService>();

            kernel.Bind<ICommentSongRepository>().To<CommentSongRepository>();
            kernel.Bind<ICommentSongService>().To<CommentSongService>();
        }
    }
}

