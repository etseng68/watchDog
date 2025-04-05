using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class MaccessLog
    {
        public long Id { get; set; }
        public DateTime LogTime { get; set; }
        public int UserId { get; set; }
        public string? UserIp { get; set; }
        public string? Note { get; set; }

        public virtual Maccount User { get; set; } = null!;
    }
}
