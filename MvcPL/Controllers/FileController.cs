using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{

    public class FileController : Controller
    {
        private IFileService FileService;
        public FileController(IFileService FileService)
        {
            this.FileService = FileService;
        }

        public FileResult GetFile(int id)
        {
            if (id > 0 )
            {
                var file = FileService.GetEntity(id);

                if (file.Data != null && file.MimeType != null)
                    return File(file.Data, file.MimeType);
            }
            var path = Server.MapPath("~/Content/images/noimagefound.jpg");
            var type = "image/jpg";

            return File(path, type);
        }


    }
}