using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_Portal.Database;
using Task_Portal.Models;

namespace Task_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var activeUser = _db.LoginUserData.FirstOrDefault(x => x.IsUserActive);
            var UserData = _db.UserRegistration.FirstOrDefault(x => x.Email == activeUser.Email);
            if(UserData != null && string.IsNullOrEmpty(UserData.ProfilePicturePath))
            {
                UserData.ProfilePicturePath = @"D:\PersonalServerForAsp\Avatar\profile.png";
            }
            TempData["UserData"] = UserData;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            var activeUser = _db.LoginUserData.FirstOrDefault(x => x.IsUserActive);
            if (activeUser != null)
            {
                activeUser.IsUserActive = false;
                _db.Update(activeUser);
                _db.SaveChanges();
            }
            return RedirectToAction("Login","Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}