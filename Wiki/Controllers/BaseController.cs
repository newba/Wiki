using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Wiki.Models.DAL;

namespace Wiki.Controllers
{
    public class BaseController : Controller
    {
        private WikiContext db;

        public BaseController()
        {
            ActualiserTitres();
        }

        public ActionResult ActualiserTitres()
        {
            db = new WikiContext();
            ViewBag.articles = db.Articles.ToList();
            return PartialView();
        }
    }
}