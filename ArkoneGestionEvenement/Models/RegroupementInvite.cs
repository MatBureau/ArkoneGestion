using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class RegroupementInvite
{
    public int IdRegroupement { get; set; }

    public string? NomRegroupement { get; set; }

    public virtual ICollection<InvitesRegroupement> InvitesRegroupements { get; set; } = new List<InvitesRegroupement>();
}
