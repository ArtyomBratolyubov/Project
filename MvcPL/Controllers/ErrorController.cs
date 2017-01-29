using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : _BaseController
    {
        
        public ActionResult Index(string msg)
        {
            return View(msg);
        }

    }
}
