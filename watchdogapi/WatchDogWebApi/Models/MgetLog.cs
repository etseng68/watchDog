using System;
using System.Collections.Generic;

namespace WatchDogWebApi.Models
{
    public partial class MgetLog
    {
        public MgetLog()
        {
            Mwatches = new HashSet<Mwatch>();
        }

        public long Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Note { get; set; }
        public DateTime? ModiyAt { get; set; }

        public virtual ICollection<Mwatch> Mwatches { get; set; }
    }
}
