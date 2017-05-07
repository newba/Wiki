using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{
    public class UtilisateursConnexionViewModel
    {
        [Display(Name = "Courriel", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "Courriel", ErrorMessageResourceType = typeof(StringArticleValidation))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "CourrielValide", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string Courriel { get; set; }

        [Display(Name = "MDP", ResourceType = typeof(StringUtilisateur))]
        [Required(ErrorMessageResourceName = "MDP", ErrorMessageResourceType = typeof(StringArticleValidation))]
        public string MDP { get; set; }

        [Display(Name = "ConnexionPersistante", ResourceType = typeof(StringUtilisateur))]
        public bool ConnexionPersistante { get; set; }
    }
}