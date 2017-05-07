using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wiki.Models.DAL;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{
    public class UtilisateurProfilViewModel
    {
        public string Courriel { get; set; }

        [Display(Name = "Prenom", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "Prenom", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string Prenom { get; set; }

        [Display(Name = "NomFamille", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "NomFamille", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string NomFamille { get; set; }

        [Display(Name = "Langue", ResourceType = typeof(StringUtilisateur))]
        public string Langue { get; set; }

        //liste de constructeurs
        public UtilisateurProfilViewModel() { }

        public UtilisateurProfilViewModel(Utilisateur utilisateur)
        {
            Courriel = utilisateur.Courriel;
            Prenom = utilisateur.Prenom;
            NomFamille = utilisateur.NomFamille;
            Langue = utilisateur.Langue;
        }
    }
}