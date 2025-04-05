using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class MwatchLog
    {
        public long Id { get; set; }
        public long? WatchId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Note { get; set; }

        public virtual Mwatch? Watch { get; set; }
    }
}
