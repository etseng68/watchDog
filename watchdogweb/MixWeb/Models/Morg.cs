using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class Morg
    {
        public Morg()
        {
            Maccounts = new HashSet<Maccount>();
            Mdomains = new HashSet<Mdomain>();
            MorgLogs = new HashSet<MorgLog>();
            Mwatches = new HashSet<Mwatch>();
        }

        public int Id { get; set; }
        public string Org { get; set; } = null!;
        public string? Note { get; set; }
        public string? Tgbot { get; set; }
        public bool Disable { get; set; }

        public virtual ICollection<Maccount> Maccounts { get; set; }
        public virtual ICollection<Mdomain> Mdomains { get; set; }
        public virtual ICollection<MorgLog> MorgLogs { get; set; }
        public virtual ICollection<Mwatch> Mwatches { get; set; }
    }
}
