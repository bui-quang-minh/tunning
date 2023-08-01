using FinalProject_PRN.Models;

namespace FinalProject_PRN.Logic
{
    public class AlbumManage
    {
        public static void CreateAlbum(Album album) {
            using (var context = new music_storeContext())
            {
                context.Albums.Add(album);
                context.SaveChanges();
            }
        }
        public static void UpdateAlbum(Album album)
        {
            using (var context = new music_storeContext())
            {
                context.Albums.Update(album);
                context.SaveChanges();
            }
        }
        public static List<Album> getAllAlbums(){
            using (var context = new music_storeContext()) { 
                List<Album> list = context.Albums.ToList();
                return list;
            }
        }
        public static Album getAlbumById(int id) {
            using (var context = new music_storeContext()) {
                return context.Albums.FirstOrDefault(x => x.AlbumId == id);
            }
        }
        public static void UploadImage(Album album, String secureUrl)
        {
            using (var context = new music_storeContext())
            {
                album.AlbumUrl= secureUrl;
                context.Albums.Update(album);
                context.SaveChanges();
            }
        }
        public static List<Album> getAlbumsByNameAndGenre(String searchString, int genreString)
        {
            using (var context = new music_storeContext())
            {
                if (searchString == null && genreString == 0)
                {
                    List<Album> list = context.Albums.ToList();
                    return list;
                }
                else if (searchString != null && genreString == 0)
                {
                    List<Album> list = context.Albums.Where(x => x.Title.Contains(searchString)).ToList();
                    return list;
                }
                else if (searchString == null && genreString > 0)
                {
                    List<Album> list = context.Albums.Where(x => x.GenreId == genreString).ToList();
                    return list;
                }
                else {
                    List<Album> list = context.Albums.Where(x => x.Title.Contains(searchString) && x.GenreId == genreString).ToList();
                    return list;
                }
            }
        }
    }
}
