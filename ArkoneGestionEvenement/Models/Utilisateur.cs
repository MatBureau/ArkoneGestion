using System;
using System.Collections.Generic;

namespace ArkoneGestionEvenement.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string? NomUtilisateur { get; set; }

    public string? MotDePasse { get; set; }

    public string? Email { get; set; }

    public bool? IsVigile { get; set; }

    public virtual ICollection<Evenement> Evenements { get; set; } = new List<Evenement>();
}
