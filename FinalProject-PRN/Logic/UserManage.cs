using FinalProject_PRN.Models;
namespace FinalProject_PRN.Logic
{
    public class UserManage
    {
        public static Boolean loginConfirmation(string uname, string psw) {
            using (var context = new music_storeContext()) {
                User user = context.Users.FirstOrDefault(x => x.Username == uname);
                if (user == null)
                {
                    return false;
                }else if (user.Password.Equals(psw)) {
                    return true;
                }else{ 
                    return false;
                }
            }
        }
        public static void CreateUser(User u) {
            using (var context = new music_storeContext())
            {
                context.Users.Add(u);
                context.SaveChanges();
            }
        }
        public static User loginInfo(string uname, string psw) {
            using (var context = new music_storeContext()) {
                return context.Users.FirstOrDefault(x => x.Username == uname && x.Password == psw);
            }
        }

        public static void UploadImage(User current_user, String secureUrl) {
            using (var context = new music_storeContext()) { 
                current_user.ImageRef = secureUrl;
                context.Users.Update(current_user);
                context.SaveChanges();
            }
        }

        public static bool UsernameAvailbility(string uname) { 
            using (var context = new music_storeContext())
            {
                if (context.Users.FirstOrDefault(x => x.Username == uname) != null) {
                    return false;
                }
                else { 
                    return true; 
                }
            }
        }

        public static List<User> GetAll() {
            using (var context = new music_storeContext())
            {
                    return context.Users.ToList();
            }
        }
    }
}
