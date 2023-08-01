using FinalProject_PRN.Models;

namespace FinalProject_PRN.Logic
{
    public class GenreManage
    {
        public static List<Genre> getAllGenres()
        {
            using (var context = new music_storeContext())
            {
                List<Genre> list = context.Genres.ToList();
                return list;
            }
        }

        public static Genre GetGenreById(int id) {
            using (var context = new music_storeContext())
            {
                return context.Genres.FirstOrDefault(x => x.GenreId == id);
            }
        }
    }
}
