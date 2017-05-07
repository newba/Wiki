using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Wiki.Resources.Models;

namespace Wiki.Models.DAL //ici modification de namespace 
{
    [MetadataType(typeof(ArticleMetaData))]
    public partial class Article
    {
        public Article(string titre)
        {
            Titre = titre;
        }


        public override string ToString()
        {
            return Titre;
        }

        public bool Ajouter(string courriel)
        {
            WikiContext db = new WikiContext();
            Models.DAL.Utilisateur utilisateur = Models.DAL.Utilisateur.RetourneUtilisateur(courriel);

            this.IdContributeur = utilisateur.Id;
            this.DateModification = DateTime.Now;
            db.Articles.Add(this);
            db.SaveChanges();

            return true;
        }

        public bool Modifier(string courriel)
        {
            WikiContext db = new WikiContext();
            Article articleModifier = db.Articles.Find(this.Titre);
            Models.DAL.Utilisateur utilisateur = Models.DAL.Utilisateur.RetourneUtilisateur(courriel);

            articleModifier.Contenu = this.Contenu;
            articleModifier.IdContributeur = utilisateur.Id;
            articleModifier.Revision += 1;
            articleModifier.DateModification = DateTime.Now;
            db.Entry(articleModifier).State = EntityState.Modified;

            db.SaveChanges();

            return true;
        }

        public bool Supprimer()
        {
            WikiContext db = new WikiContext();
            Article articleSupprimer = db.Articles.Find(this.Titre);

            db.Entry(articleSupprimer).State = EntityState.Deleted;

            db.SaveChanges();

            return true;
        }


        private class ArticleMetaData
        {
            [Required]
            [Display(Name = "Titre", ResourceType = typeof(StringArticle))]
            public string Titre { get; set; }

            [Required]
            [Display(Name = "Contenu", ResourceType = typeof(StringArticle))]
            public string Contenu { get; set; }

            [Display(Name = "DateModification", ResourceType = typeof(StringArticle))]
            public DateTime DateModification { get; set; }

            [Display(Name = "Revision", ResourceType = typeof(StringArticle))]
            public int Revision { get; set; }

            [Display(Name = "IdContributeur", ResourceType = typeof(StringArticle))]
            public int IdContributeur { get; set; }
        }
    }
}