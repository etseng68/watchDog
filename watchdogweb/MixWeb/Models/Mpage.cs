using System;
using System.Collections.Generic;

namespace MixWeb.Models
{
    public partial class Mpage
    {
        public int Id { get; set; }
        public string? Main { get; set; }
        public string? Sub { get; set; }
        public string? Path { get; set; }
        public string? Note { get; set; }
        public int? Role { get; set; }
    }
}
