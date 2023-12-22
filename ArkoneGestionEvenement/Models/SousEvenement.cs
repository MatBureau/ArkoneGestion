using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class SousEvenement
{
    public int IdSousEvent { get; set; }

    public int? IdEvent { get; set; }

    public string? NomSousEvent { get; set; }

    public DateTime? DateHeure { get; set; }

    public virtual Evenement? IdEventNavigation { get; set; }
}
