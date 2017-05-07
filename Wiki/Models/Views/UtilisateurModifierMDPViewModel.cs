using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{
    public class UtilisateurModifierMDPViewModel
    {
        public string Courriel { get; set; }

        [Display(Name = "AncienMDP", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "AncienMDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string AncienMDP { get; set; }

        [Display(Name = "MDP", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "MDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string MDP { get; set; }

        //reverification du mot de passe
        [Display(Name = "MDPConfirmation", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "MDPConfirmation", ErrorMessageResourceType = typeof(StringArticleValidation))]
        [Compare("MDP", ErrorMessageResourceName = "ComparaisonMDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string MDPConfirmation { get; set; }
    }
}