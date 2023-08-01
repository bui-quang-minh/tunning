using System;
using System.Collections.Generic;

namespace FinalProject_PRN.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Albums = new HashSet<Album>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
