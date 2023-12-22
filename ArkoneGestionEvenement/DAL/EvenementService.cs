using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class EvenementService
    {
        public static ObservableCollection<Evenement> GetAllEvenement()
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                List<Evenement> lst = db.Evenements.ToList();
                return new ObservableCollection<Evenement>(lst); 
            }
        }

        public static Evenement AddEvenement(Evenement eventToAdd)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.Evenements.Add(eventToAdd);
                db.SaveChanges();
                return eventToAdd;
            }
        }
    }
}
