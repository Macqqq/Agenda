using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Contactsocialmedium
{
    public int IdContactSocialMedia { get; set; }

    public string Username { get; set; } = null!;

    public string? Url { get; set; }

    public int SocialMediaIdSocialMedia { get; set; }

    public int SocialMediaContactstableIdContactstable { get; set; }

    public int ContactstableIdContactstable { get; set; }
}
