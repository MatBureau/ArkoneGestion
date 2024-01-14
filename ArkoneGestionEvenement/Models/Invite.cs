using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class Invite
{
    public int IdInvite { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public bool? Confirmation { get; set; }

    public virtual ICollection<CodesAcce> CodesAcces { get; set; } = new List<CodesAcce>();

    public virtual ICollection<InvitesRegroupement> InvitesRegroupements { get; set; } = new List<InvitesRegroupement>();
}
