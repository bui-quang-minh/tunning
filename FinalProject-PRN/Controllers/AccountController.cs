using CloudinaryDotNet.Actions;
using FinalProject_PRN.Logic;
using FinalProject_PRN.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PRN.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string param)
        {
            return View();
        }
        public IActionResult LoginRedirect(string uname , string psw)
        {
            if (UserManage.loginConfirmation(uname, psw)) {
                User u = UserManage.loginInfo(uname, psw);
                HttpContext.Session.Set<User>("current_user", u);
                if (u.IsAdmin)
                {
                    return RedirectToAction("AdminSite", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }else {
                ViewData["errStr"] = "Username or password is incorrect!";
                return View("Login");
            }            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("current_user");
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccountSetting()
        {
            User current_user = HttpContext.Session.Get<User>("current_user");
            if (current_user != null)
            {
                ViewBag.current_user = current_user;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ImageUpload(IFormFile uploadFile)
        {
            User current_user = HttpContext.Session.Get<User>("current_user");
            CloudinarySettings cs = new CloudinarySettings();
            ImageUploadResult res = cs.CloudinaryUpload(uploadFile);
            UserManage.UploadImage(current_user, res.SecureUrl.ToString());
            current_user.ImageRef=res.SecureUrl.ToString();
            HttpContext.Session.Remove("current_user");
            HttpContext.Session.Set<User>("current_user", current_user);
            return RedirectToAction("AccountSetting");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAccount(string uname, string fname, string lname, string psw, string cpsw)
        {
            bool flag = false;
            if (!UserManage.UsernameAvailbility(uname)) { flag = true; ViewData["unameErr"] = "Username already taken!"; }
            if (psw != cpsw) { flag = true; ViewData["pswErr"] = "Password miss match!"; }
            if (flag) {
                return View("Register");
            }
            else
            {
                User u = new User();
                u.Username = uname;
                u.Password = psw;
                u.FirstName = fname;
                u.LastName = lname;
                u.ImageRef = "https://inkythuatso.com/uploads/images/2022/03/hinh-avatar-trang-cho-nu-30-10-05-19.jpg";
                UserManage.CreateUser(u);
                ViewData["unameErr"] = "";
                ViewData["pswErr"] = "";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
