using Microsoft.AspNetCore.Mvc;
using Task_Portal.Database;
using Task_Portal.Models;

namespace Task_Portal.Controllers
{
    public class LoginController : Controller
    {
        #region Properties
        private readonly ApplicationDBContext _db;
        #endregion

        #region Constructor
        public LoginController(ApplicationDBContext db)
        {
            _db = db;
        }
        #endregion
        public IActionResult Login()
        {
            IActionResult _view = View();
            var activeUser = _db.LoginUserData.FirstOrDefault(x => x.IsUserActive);
            if (activeUser == null)
                return _view;
            var LogOutDate = activeUser.LoginDate == null ? DateTime.Now.AddMinutes(AppConfig.AppConfig.Auto_Logout) : activeUser.LoginDate.AddMinutes(AppConfig.AppConfig.Auto_Logout);
            if (activeUser != null && DateTime.Now < LogOutDate)
            {
                TempData["success"] = null;
                _view = RedirectToAction("Index", "Home", activeUser);
            }
            return _view;
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            IActionResult _view = View(login);
            if (login != null && ModelState.IsValid)
            {
                var loginInfo = _db.LoginUserData.Any(x => x.Email == login.Email && x.LoginPassword == login.LoginPassword);
                var loginInfoData = _db.LoginUserData.FirstOrDefault(x => x.Email == login.Email && x.LoginPassword == login.LoginPassword);
                if (loginInfo)
                {
                    TempData["success"] = "Logging in";
                    loginInfoData.LoginDate = DateTime.Now;
                    loginInfoData.IsUserActive = true;
                    _db.Update(loginInfoData);
                    _db.SaveChanges();
                    _view = RedirectToAction("Index", "Home", loginInfoData);
                }
                else
                {
                    TempData["error"] = "Please Enter registered Email address and Password";
                }
            }
            return _view;
        }
        [HttpPost]
        public IActionResult Create(UserRegistrationViewModel userRegistration)
        {
            IActionResult _view = View("Login");
            try
            {
                if (ModelState.IsValid)
                {
                    if (userRegistration.Password != userRegistration.Confirm_Password)
                    {
                        TempData["error"] = "Please Enter Exact same password for Confirm Password";
                        userRegistration.Confirm_Password = string.Empty;
                        return _view;
                    }

                    if(!string.IsNullOrEmpty(userRegistration.Phone))
                    {
                        var phoneExist = _db.UserRegistration.Any(x => x.Phone == userRegistration.Phone);
                        if(phoneExist)
                        {
                            TempData["error"] = "This Phone number already exist please try with anoter phone";
                            return _view;
                        }
                    }

                    TempData["success"] = "User Registered";
                    LoginViewModel login = new LoginViewModel();
                    login.Email = userRegistration.Email;
                    login.LoginPassword = userRegistration.Password;
                    _db.LoginUserData.Add(login);
                    _db.UserRegistration.Add(userRegistration);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    var invalidValues = ModelState.Where(x => x.Value != null && x.Value.Errors?.Count > 0).ToList();
                    foreach (var item in invalidValues)
                    {
                        TempData["error"] = $"{item.Value.Errors[0].ErrorMessage}";
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
            return _view;
        }
    }
}
