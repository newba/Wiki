using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{
    public class UtilisateurInscriptionViewModel
    {
        public static string[] Langues = { "fr-CA", "en-CA" };

        [Display(Name = "Prenom", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "Prenom", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string Prenom { get; set; }

        [Display(Name = "NomFamille", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "NomFamille", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string NomFamille { get; set; }

        [Display(Name = "Courriel", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "Courriel", ErrorMessageResourceType = typeof(StringArticleValidation))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "CourrielValide", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string Courriel { get; set; }

        [Display(Name = "MDP", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "MDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        //[RegularExpression("", ErrorMessage = "Le mot de passe doit au moins contenir une lettre, un chiffre et un caractère spécial")]// Ce n'est pas fini
        public string MDP { get; set; }

        [Display(Name = "MDPConfirmation", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "MDPConfirmation", ErrorMessageResourceType = typeof(StringArticleValidation))]
        [Compare("MDP", ErrorMessageResourceName = "ComparaisonMDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string MDPConfirmation { get; set; }
        
        [Display(Name = "Langue", ResourceType = typeof(StringUtilisateur))]
        public string Langue { get; set; }
    }
}