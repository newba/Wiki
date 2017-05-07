using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.DAL;
using System.Data.Entity;

namespace Wiki.Controllers
{
    [Authorize] //http://stackoverflow.com/questions/9581270/is-it-possible-to-disable-authorization-on-one-action-in-an-mvc-controller
    public class HomeController : BaseController
    {
        WikiContext db = new WikiContext();

        // GET: Home
        [HttpGet]
        [AllowAnonymous] //voir le lien au-dessus de la class
        public ActionResult Index(string id)
        {
            if (ModelState.IsValid)
            {
                if(null == id)
                {
                    return View();
                }
                else if(id != null && db.Articles.Find(id) == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Ajouter", new { id = id });
                    }
                    else
                    {
                        return RedirectToAction("Connexion", "Utilisateurs", new { returnUrl = "/Home/Ajouter/" + id});
                    }
                }
                else
                {
                    return View(db.Articles.Find(id));
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Home/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id) 
        {
            return View(db.Articles.Find(id));
        }
        
        // GET: Home/Create
        public ActionResult Ajouter(string id)
        {
            return View(new Article(id));
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ajouter(string soumettre, string id, Article article) 
        {
            try
            {
                //TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    //https://www.google.ca/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=button+submit+how+to+know+which+one+mvc+actionresult
                    //http://stackoverflow.com/questions/1714028/mvc-which-submit-button-has-been-pressed
                    switch (soumettre)
                    {
                        case "Ajouter":
                            article.Ajouter(User.Identity.Name);
                            ViewBag.Preview = false;

                            return RedirectToAction("Index");

                        case "Aperçu":

                            ViewBag.Preview = true;

                            return View("Ajouter", article);
                    }
                }

                return View("Error");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // GET: Home/Edit/5
        public ActionResult Modifier(string id) 
        {
            return View(db.Articles.Find(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modifier(string soumettre, string id, Article article) 
        {
            try
            {
                // TODO: Add insert logic here
                switch (soumettre)
                {
                    case "Sauvegarder":
                        article.Modifier(User.Identity.Name);
                        ViewBag.Preview = false;

                        return RedirectToAction("Index");

                    case "Aperçu":
                        ViewBag.Preview = true;

                        return View("Modifier", article);
                }

                return View("Error");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // GET: Home/Delete/5
        public ActionResult Supprimer(string id) 
        {
            return View(db.Articles.Find(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Supprimer(string id, Article article) 
        {
            try
            {
                // TODO: Add delete logic here
                article.Supprimer();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        
        [AllowAnonymous]
        public ActionResult ChangerLangue(string langue, string returnUrl)
        {
            HttpCookie cookie = Request.Cookies["langue"];
            if (cookie == null)
                cookie = new HttpCookie("langue");

            cookie.Value = langue;
            Response.Cookies.Add(cookie);

            //http://stackoverflow.com/questions/9772947/c-sharp-asp-net-mvc-return-to-previous-page
            if(returnUrl == null)
                return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
            else
                return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
