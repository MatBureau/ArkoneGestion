using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class ConnexionService
    {
        public static Utilisateur SelectUtilisateurByNameAndPwd(string userName, string password)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                Utilisateur utilisateur = db.Utilisateurs.SingleOrDefault(j => j.NomUtilisateur == userName);

                if (utilisateur != null && utilisateur.MotDePasse == password)
                {
                    return utilisateur;
                }
                return null;
            }
        }

        public static void AddUtilisateur(Utilisateur utilisateur)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
            }
        }
    }
}
