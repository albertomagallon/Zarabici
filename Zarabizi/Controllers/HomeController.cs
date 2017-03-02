using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zarabizi.Resources;

namespace Zarabizi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = @Messages.lbl_Bienvenido;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
