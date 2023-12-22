using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class CodesAcce
{
    public int IdCodeAcces { get; set; }

    public string? Code { get; set; }

    public int? IdInvite { get; set; }

    public int? IdEvent { get; set; }

    public string? Statut { get; set; }

    public virtual Evenement? IdEventNavigation { get; set; }

    public virtual Invite? IdInviteNavigation { get; set; }
}
