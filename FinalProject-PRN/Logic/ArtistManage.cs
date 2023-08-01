using FinalProject_PRN.Models;

namespace FinalProject_PRN.Logic
{
    public class ArtistManage
    {
        public static Artist getArtistById(int id) {
            using (var context = new music_storeContext()) {
                return context.Artists.FirstOrDefault(x => x.ArtistId == id);
            }
        }
        public static List<Artist> getAllArtists() {
            using (var context = new music_storeContext())
            {
                return context.Artists.ToList();    
            }
        }

    }
}
