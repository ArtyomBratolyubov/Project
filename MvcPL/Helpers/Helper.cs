using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Helpers
{
    public class Helper
    {
        public static readonly int pageSize = 10;

        public static int GetCountOfPages(int count)
        {
            int pages = count / pageSize;
            return count % pageSize == 0 ? pages : pages + 1;
        }

        public static bool CheckImae(HttpPostedFileBase file)
        {
            return file.ContentType == "File/jpg" ||
                  file.ContentType == "File/png" ||
                  file.ContentType == "File/jpeg";
        }
    }
}