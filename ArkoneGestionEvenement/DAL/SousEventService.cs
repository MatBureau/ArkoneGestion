using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class SousEventService
    {
        public static ObservableCollection<SousEvenement> GetSousEvenement(int evenementID)
        {
            using(ArkoneGestionContext db = new ArkoneGestionContext())
            {
                List<SousEvenement> lst = db.SousEvenements.Where(s => s.IdEvent == evenementID).ToList();
                return new ObservableCollection<SousEvenement>(lst);
            }
        }

        public static int AddSousEvent(SousEvenement se)
        {
            using(ArkoneGestionContext context = new ArkoneGestionContext())
            {
                context.SousEvenements.Add(se);
                context.SaveChanges();
                return se.IdSousEvent;
            }
        }
    }
}
