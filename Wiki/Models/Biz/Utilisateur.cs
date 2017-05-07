
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Wiki.Models.Views;
using Wiki.Models.Biz;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;

namespace Wiki.Models.DAL
{
    [MetadataType(typeof(MetaDataUtilisateur))]
    public partial class Utilisateur
    {
        public Utilisateur(UtilisateurInscriptionViewModel inscriptionViewModel)
        {
            Prenom = inscriptionViewModel.Prenom;
            NomFamille = inscriptionViewModel.NomFamille;
            Courriel = inscriptionViewModel.Courriel;
            MDP = inscriptionViewModel.MDP;
            Langue = inscriptionViewModel.Langue;
        }

        public Utilisateur(UtilisateurProfilViewModel profilViewModel)
        {
            Prenom = profilViewModel.Prenom;
            NomFamille = profilViewModel.NomFamille;
            Courriel = profilViewModel.Courriel;
            Langue = profilViewModel.Langue;
        }


        public static bool EstValide(string courriel, string motDePasse)
        {
            bool resultat = false;
            Utilisateur utilisateurTrouve = RetourneUtilisateur(courriel);

            if (utilisateurTrouve != null)
            {
                resultat = PasswordHash.ValidatePassword(motDePasse, utilisateurTrouve.MDP);
            }

            return resultat;
        }

        public static Utilisateur RetourneUtilisateur(string courriel)
        {
            WikiContext db = new WikiContext();

            return db.Utilisateurs.Where(u => u.Courriel == courriel).FirstOrDefault();
        }

        public static Utilisateur Inscrire(UtilisateurInscriptionViewModel utilisateurInscription)
        {
            WikiContext db = new WikiContext();

            utilisateurInscription.MDP = PasswordHash.CreateHash(utilisateurInscription.MDP);

            Utilisateur utilisateur = new Utilisateur(utilisateurInscription);

            db.Utilisateurs.Add(utilisateur);

            db.SaveChanges();

            return utilisateur;
        }

        public static Utilisateur ModifierProfil(UtilisateurProfilViewModel utilisateurModificationP)
        {
            WikiContext db = new WikiContext();
            Utilisateur utilisateur = db.Utilisateurs.Where(u => u.Courriel == utilisateurModificationP.Courriel).FirstOrDefault();

            utilisateur.Prenom = utilisateurModificationP.Prenom;
            utilisateur.NomFamille = utilisateurModificationP.NomFamille;
            utilisateur.Langue = utilisateurModificationP.Langue;
            db.Entry(utilisateur).State = EntityState.Modified;

            db.SaveChanges();

            return utilisateur;
        }

        public static bool ModifierMDP(UtilisateurModifierMDPViewModel utilisateurModifierMDP)
        {
            WikiContext db = new WikiContext();

            utilisateurModifierMDP.MDP = PasswordHash.CreateHash(utilisateurModifierMDP.MDP);

            Utilisateur utilisateur = db.Utilisateurs.Where(u => u.Courriel == utilisateurModifierMDP.Courriel).FirstOrDefault();

            utilisateur.MDP = utilisateurModifierMDP.MDP;
            db.Entry(utilisateur).State = EntityState.Modified;

            db.SaveChanges();

            return true;
        }

        public static List<SelectListItem> RetourneSelectListItemLangues()
        {
            List<SelectListItem> langues = Langues.ToList().ConvertAll(l => { return new SelectListItem() { Text = l.ToString(), Value = l.ToString() }; });

            return langues;
        }

        public static string[] Langues = { "fr-CA", "en-US", "es-US", "pt-BR" };


        private class MetaDataUtilisateur
        {
            public int Id { get; set; }

            public string Prenom { get; set; }

            public string NomFamille { get; set; }

            public string Courriel { get; set; }

            public string MDP { get; set; }

            public string Langue { get; set; }
        }
    }
}