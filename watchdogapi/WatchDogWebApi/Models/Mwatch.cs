using System;
using System.Collections.Generic;

namespace WatchDogWebApi.Models
{
    public partial class Mwatch
    {
        public long Id { get; set; }
        public string Url { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Wtime { get; set; } = null!;
        public string Org { get; set; } = null!;
        public int? DomainId { get; set; }
        public DateTime? WdateTime { get; set; }
        public int? OrgId { get; set; }
        public DateTime? CreateAt { get; set; }
        public long? GetId { get; set; }

        public virtual Mdomain? Domain { get; set; }
        public virtual MgetLog? Get { get; set; }
        public virtual Morg? OrgNavigation { get; set; }
    }
}
