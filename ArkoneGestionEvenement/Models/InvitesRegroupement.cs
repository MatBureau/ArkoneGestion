using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class InvitesRegroupement
{
    public int Id { get; set; }

    public int? IdInvite { get; set; }

    public int? IdRegroupement { get; set; }

    public virtual Invite? IdInviteNavigation { get; set; }

    public virtual RegroupementInvite? IdRegroupementNavigation { get; set; }
}
