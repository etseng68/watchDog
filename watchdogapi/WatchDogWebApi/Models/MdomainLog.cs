using System;
using System.Collections.Generic;

namespace WatchDogWebApi.Models
{
    public partial class MdomainLog
    {
        public long Id { get; set; }
        public int? DomainId { get; set; }
        public string? Note { get; set; }
        public int? UrserId { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Mdomain? Domain { get; set; }
        public virtual Maccount? Urser { get; set; }
    }
}
