using ArkoneGestionEvenement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class InviteService
    {
        public static ObservableCollection<Invite> GetAllInvite()
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                List<Invite> invites = db.Invites.ToList();
                return new ObservableCollection<Invite>(invites);
            }
        }

        public static Invite AddInvite(Invite inviteToAdd)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.Invites.Add(inviteToAdd);
                db.SaveChanges();
                return inviteToAdd;
            }
        }

        public static Invite UpdateInvite(Invite inviteToUpdate)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.Entry(inviteToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return inviteToUpdate;
            }
        }

        public static void DeleteInvite(Invite inviteToDelete)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.Invites.Remove(inviteToDelete);
                db.SaveChanges();
            }
        }

        public static Invite GetInvite(int id)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.Invites.Find(id);
            }
        }

        public static Invite GetInviteByMail(string mail)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.Invites.Where(i => i.Email == mail).FirstOrDefault();
            }
        }
    }
}
