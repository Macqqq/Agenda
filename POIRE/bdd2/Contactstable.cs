using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Contactstable
{
    public int IdContactstable { get; set; }

    public string Name { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Phone { get; set; }

    public string? Adresse { get; set; }

    public int? CodePostal { get; set; }

    public string? Ville { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<Evenement> Evenements { get; set; } = new List<Evenement>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Socialmedium> Socialmedia { get; set; } = new List<Socialmedium>();
}
