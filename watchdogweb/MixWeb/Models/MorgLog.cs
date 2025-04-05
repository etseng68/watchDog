using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class MorgLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OrgId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Note { get; set; }

        public virtual Morg? Org { get; set; }
        public virtual Maccount? User { get; set; }
    }
}
