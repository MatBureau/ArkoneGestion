using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class InviteRegroupeService
    {
        public static List<InvitesRegroupement> GetAllInviteRegroupe()
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.InvitesRegroupements.ToList();
            }
        }

        public static InvitesRegroupement AddInviteRegroupe(InvitesRegroupement inviteRegroupeToAdd)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.InvitesRegroupements.Add(inviteRegroupeToAdd);
                db.SaveChanges();
                return inviteRegroupeToAdd;
            }
        }

        public static InvitesRegroupement GetInviteRegroupeById(int id)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.InvitesRegroupements.Find(id);
            }
        }
        
        public static List<InvitesRegroupement> GetInviteRegroupeByRegroupementId(int regroupementId)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.InvitesRegroupements.Where(i => i.IdRegroupement == regroupementId).ToList();
            }
        }

        public static List<InvitesRegroupement> GetInviteRegroupeByInviteId(int Inviteid)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.InvitesRegroupements.Where(i => i.IdInvite == Inviteid).ToList();
            }
        }
    }
}
