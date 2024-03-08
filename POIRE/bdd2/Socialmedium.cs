using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Socialmedium
{
    public int IdSocialMedia { get; set; }

    public string? Instagram { get; set; }

    public string? Facebook { get; set; }

    public string? XTwitter { get; set; }

    public string? Youtube { get; set; }

    public string? Twitch { get; set; }

    public string? Discord { get; set; }

    public string? Tiktok { get; set; }

    public int ContactstableIdContactstable { get; set; }

    public virtual Contactstable ContactstableIdContactstableNavigation { get; set; } = null!;
}
