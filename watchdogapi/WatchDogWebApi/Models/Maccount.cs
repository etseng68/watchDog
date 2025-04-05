using System;
using System.Collections.Generic;

namespace WatchDogWebApi.Models
{
    public partial class Maccount
    {
        public Maccount()
        {
            MaccessLogs = new HashSet<MaccessLog>();
            MdomainLogs = new HashSet<MdomainLog>();
            MorgLogs = new HashSet<MorgLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public int Role { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ModifyAt { get; set; }
        public string? Note { get; set; }
        public string? Org { get; set; }
        public bool Disable { get; set; }

        public virtual Morg? OrgNavigation { get; set; }
        public virtual Mpermission RoleNavigation { get; set; } = null!;
        public virtual ICollection<MaccessLog> MaccessLogs { get; set; }
        public virtual ICollection<MdomainLog> MdomainLogs { get; set; }
        public virtual ICollection<MorgLog> MorgLogs { get; set; }
    }
}
