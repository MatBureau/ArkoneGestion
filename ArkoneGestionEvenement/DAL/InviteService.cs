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
    }
}
