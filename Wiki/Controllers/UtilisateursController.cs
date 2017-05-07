using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.Biz;
using Wiki.Models.DAL;
using Wiki.Models.Views;
using System.Web.Security;

namespace Wiki.Controllers
{
    public class UtilisateursController : Controller
    {
        WikiContext db = new WikiContext();
        Utilisateur utilisateurActuel;

        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View();
        }
        
        // GET
        public ActionResult Connexion(string returnUrl = "")
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connexion(UtilisateursConnexionViewModel utilisateurConnexionViewModel, bool connexionPersistante, string returnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                if (Utilisateur.EstValide(utilisateurConnexionViewModel.Courriel, utilisateurConnexionViewModel.MDP))
                {
                    FormsAuthentication.SetAuthCookie(utilisateurConnexionViewModel.Courriel, connexionPersistante);

                    utilisateurActuel = Utilisateur.RetourneUtilisateur(utilisateurConnexionViewModel.Courriel);

                    return RedirectToAction("ChangerLangue", "Home", new { langue = utilisateurActuel.Langue, returnUrl = returnUrl });
                }
            }

            return View(utilisateurConnexionViewModel);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // GET
        public ActionResult Inscription()
        {
            return View(new UtilisateurInscriptionViewModel());
        }

        [HttpPost]
        public ActionResult Inscription(UtilisateurInscriptionViewModel utilisateurInscriptionViewModel, string returnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                utilisateurActuel = Utilisateur.Inscrire(utilisateurInscriptionViewModel);

                FormsAuthentication.SetAuthCookie(utilisateurInscriptionViewModel.Courriel, false);

                return RedirectToAction("ChangerLangue", "Home", new { langue = utilisateurActuel.Langue, returnUrl = returnUrl });
            }

            return View(utilisateurInscriptionViewModel);
        }

        // GET
        public ActionResult Profil()
        {
            UtilisateurProfilViewModel utiliModif = new UtilisateurProfilViewModel(Utilisateur.RetourneUtilisateur(User.Identity.Name));

            return View(utiliModif);
        }

        [HttpPost]
        public ActionResult Profil(UtilisateurProfilViewModel utilisateurProfilViewModel, string returnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                utilisateurProfilViewModel.Courriel = User.Identity.Name;

                utilisateurActuel = Utilisateur.ModifierProfil(utilisateurProfilViewModel);

                return RedirectToAction("ChangerLangue", "Home", new { langue = utilisateurActuel.Langue, returnUrl = returnUrl });
            }

            return View(utilisateurProfilViewModel);
        }
        
        // GET
        [Authorize]
        public ActionResult ModifierMDP()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ModifierMDP(UtilisateurModifierMDPViewModel utilisateurModifMDPViewModel, string returnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                utilisateurModifMDPViewModel.Courriel = User.Identity.Name;

                if (Utilisateur.EstValide(utilisateurModifMDPViewModel.Courriel, utilisateurModifMDPViewModel.AncienMDP))
                {
                    Utilisateur.ModifierMDP(utilisateurModifMDPViewModel);

                    return Redirect(returnUrl);
                }
            }
            return Redirect(returnUrl);
        }
    }
}