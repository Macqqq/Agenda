using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Evenement
{
    public int IdEvenements { get; set; }

    public string? Titre { get; set; }

    public string? Description { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public int ContactstableIdContactstable { get; set; }

    public virtual Contactstable ContactstableIdContactstableNavigation { get; set; } = null!;
}
