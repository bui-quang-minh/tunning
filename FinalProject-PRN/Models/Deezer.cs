using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace FinalProject_PRN.Models
{
    public class Deezer
    {
        public Deezer()
        {
        }

        public int id { get; set; }
        public bool readable { get; set; }
        public string title { get; set; }
        public string title_short { get; set; }
        public string title_version { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public int rank { get; set; }
        public bool explicit_lyrics { get; set; }
        public string preview { get; set; }
        public virtual Artist artist { get; set; }
        public virtual Album album { get; set; }
    }
}

