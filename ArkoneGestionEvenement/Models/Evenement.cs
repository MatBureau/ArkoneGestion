using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class Evenement
{
    public int IdEvent { get; set; }

    public string? Nom { get; set; }

    public string? Lieu { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? DateEvenement { get; set; }

    public int? IdOrganisateur { get; set; }

    public virtual ICollection<CodesAcce> CodesAcces { get; set; } = new List<CodesAcce>();

    public virtual Utilisateur? IdOrganisateurNavigation { get; set; }

    public virtual ICollection<SousEvenement> SousEvenements { get; set; } = new List<SousEvenement>();
}
