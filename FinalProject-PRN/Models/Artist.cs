using System;
using System.Collections.Generic;

namespace FinalProject_PRN.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int ArtistId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Album> Albums { get; set; }
    }
}
