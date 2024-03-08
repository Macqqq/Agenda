using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Note
{
    public int IdNotes { get; set; }

    public string? Contenu { get; set; }

    public DateTime? DateCreation { get; set; }

    public int ContactstableIdContactstable { get; set; }

    public virtual Contactstable ContactstableIdContactstableNavigation { get; set; } = null!;
}
