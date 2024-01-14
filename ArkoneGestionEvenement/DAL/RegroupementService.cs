using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class RegroupementService
    {
        public static List<RegroupementInvite> GetAllRegroupement()
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.RegroupementInvites.ToList();
            }
        }

        public static RegroupementInvite AddRegroupement(RegroupementInvite regroupementToAdd)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.RegroupementInvites.Add(regroupementToAdd);
                db.SaveChanges();
                return regroupementToAdd;
            }
        }

        public static RegroupementInvite GetRegroupementById(int id)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.RegroupementInvites.Find(id);
            }
        }
    }
}
