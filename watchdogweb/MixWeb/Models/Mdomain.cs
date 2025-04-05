using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class Mdomain
    {
        public Mdomain()
        {
            MdomainLogs = new HashSet<MdomainLog>();
            Mwatches = new HashSet<Mwatch>();
        }

        public int Id { get; set; }
        public string? Wname { get; set; }
        public string Wurl { get; set; } = null!;
        public int Rtime { get; set; }
        public int? Etime { get; set; }
        public bool? Wssl { get; set; }
        public bool? Essl { get; set; }
        public string Wact { get; set; } = null!;
        public string Org { get; set; } = null!;
        public string? Wmemo { get; set; }
        public bool Wrun { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public bool Del { get; set; }

        public virtual Morg OrgNavigation { get; set; } = null!;
        public virtual ICollection<MdomainLog> MdomainLogs { get; set; }
        public virtual ICollection<Mwatch> Mwatches { get; set; }
    }
}
