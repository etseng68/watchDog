using System;
using System.Collections.Generic;

namespace WatchDogWebApi.Models
{
    public partial class Mpermission
    {
        public Mpermission()
        {
            Maccounts = new HashSet<Maccount>();
        }

        public int Id { get; set; }
        public string Role { get; set; } = null!;
        public string? Note { get; set; }
        public bool Disable { get; set; }

        public virtual ICollection<Maccount> Maccounts { get; set; }
    }
}
