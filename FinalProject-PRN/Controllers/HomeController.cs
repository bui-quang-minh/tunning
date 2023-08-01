using CloudinaryDotNet.Actions;
using FinalProject_PRN.Logic;
using FinalProject_PRN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace FinalProject_PRN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult ExportInvoice() {

            return RedirectToAction("Index");
        }
        public IActionResult CheckOut(string fname, string lname, string email, string pnum, string country, string city, string state, string h_address) {
            List<CartItem> carts = HttpContext.Session.Get<List<CartItem>>("cart") ?? new List<CartItem>();
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.carts = carts;
                Models.Order o = new Models.Order();
                o.FirstName = fname;
                o.LastName = lname;
                o.Email = email;
                o.Phone = pnum;
                o.Country = country;
                o.City = city;
                o.State = state;
                o.Address = h_address;
                o.UserId = current_user.UserId;
                OrderManage.addOrder(o, carts);
                ViewBag.order = o;
                HttpContext.Session.Remove("cart");
                ViewBag.current_user = current_user;
                return View();
            }
        }
        public IActionResult Cart()
        {
            List<CartItem> carts = HttpContext.Session.Get<List<CartItem>>("cart") ?? new List<CartItem>();
            User current_user = HttpContext.Session.Get<User>("current_user");
            ViewBag.current_user = current_user;
            ViewBag.carts = carts;
            return View();
        }

        public IActionResult AddToCart(string searchString, string genreString, int aid, int number)
        {
            List<CartItem> carts = HttpContext.Session.Get<List<CartItem>>("cart") ?? new List<CartItem>();
            Album a = AlbumManage.getAlbumById(aid);
            CartItem item = carts.Where(x => x.AlbumId == aid).FirstOrDefault();
            User current_user = HttpContext.Session.Get<User>("current_user");
            ViewBag.current_user = current_user;
            if (item != null)
            {
                carts.Remove(item);
                item.Quantity += number;
                carts.Add(item);
            }
            else {
                CartItem newCart = new CartItem();
                newCart.AlbumId = aid;
                newCart.UnitPrice = a.Price;
                newCart.Quantity = number;
                carts.Add(newCart);
            }
            HttpContext.Session.Set<List<CartItem>>("cart", carts);
            if (searchString == "0" && genreString == "0") return RedirectToAction("Search");
            return RedirectToAction("Search", "Home", new { searchString, genreString });
        }
        public IActionResult Index()
        {
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user != null)
            {
                ViewBag.current_user = current_user;
            }
            ViewBag.albums = AlbumManage.getAllAlbums();
            ViewBag.genres = GenreManage.getAllGenres();
            return View();
        }

        public IActionResult Search(String searchString, String genreString)
        {
            if (genreString == null) {
                genreString = "0";
            }
            List<CartItem> carts = HttpContext.Session.Get<List<CartItem>>("cart") ?? new List<CartItem>();
            ViewBag.albums = AlbumManage.getAlbumsByNameAndGenre(searchString, Convert.ToInt32(genreString));
            ViewBag.genres = GenreManage.getAllGenres();
            ViewData["carts"] = carts;
            ViewData["searchString"] = searchString;
            ViewData["genreString"] = genreString;
            return View();
        }

        public IActionResult GetDetails(int oid) {
            ViewBag.orderdetails = OrderDetailManage.GetClientOrderDetails(oid);
            ViewBag.orders = OrderManage.GetOrderById(oid);
            return View();
        }
        public IActionResult Order() {
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user != null)
            {
                ViewBag.current_user = current_user;
                ViewBag.orders = OrderManage.GetClientOrders(current_user.UserId);
                ViewBag.orderdetails = OrderDetailManage.GetClientOrderDetails(OrderManage.GetClientOrders(current_user.UserId));
            }
            ViewBag.albums = AlbumManage.getAllAlbums();
            ViewBag.genres = GenreManage.getAllGenres();
            return View();
        }
        public IActionResult AlbumDetails(int aid) {
            ViewBag.album = AlbumManage.getAlbumById(aid);
            ViewBag.allalbums = AlbumManage.getAllAlbums();
            return View();
        }

        public IActionResult AdminSite()
        {
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user != null)
            {
                ViewBag.allalbums = AlbumManage.getAllAlbums();
                ViewBag.current_user = current_user;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult AlbumChange(int aid)
        {
            ViewBag.artists = ArtistManage.getAllArtists();
            ViewBag.genres = GenreManage.getAllGenres();
            ViewBag.album = AlbumManage.getAlbumById(aid);
            return View();
        }

        [HttpPost]
        public IActionResult AlbumImageUpload(IFormFile uploadFile, int aid) {
            CloudinarySettings cs = new CloudinarySettings();
            ImageUploadResult res = cs.CloudinaryUpload(uploadFile);
            AlbumManage.UploadImage(AlbumManage.getAlbumById(aid), res.SecureUrl.ToString());
            return RedirectToAction("AdminSite");
        }
        [HttpPost]
        public IActionResult ChangeAlbumAttributes(int aid, decimal price, string title, string genre, string artist) {
            Album album = AlbumManage.getAlbumById(aid);
            album.Title = title;
            album.GenreId = Int32.Parse(genre);
            album.Price = price;
            album.ArtistId = Int32.Parse(artist);
            AlbumManage.UpdateAlbum(album);
            return RedirectToAction("AdminSite");
        }

        public IActionResult AddAlbum() {
            ViewBag.artists = ArtistManage.getAllArtists();
            ViewBag.genres = GenreManage.getAllGenres();
            return View();
        }
        [HttpPost]
        public IActionResult AddAlbumAttributes(decimal price, string title, string genre, string artist) {
            Album album = new Album();
            album.Title = title;
            album.GenreId = Int32.Parse(genre);
            album.Price = price;
            album.ArtistId = Int32.Parse(artist);
            AlbumManage.CreateAlbum(album);
            return RedirectToAction("AdminSite");
        }

        public IActionResult AllCustomer()
        {
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user != null)
            {
                ViewBag.customers = UserManage.GetAll();
                ViewBag.allalbums = AlbumManage.getAllAlbums();
                ViewBag.current_user = current_user;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        
    }
}
