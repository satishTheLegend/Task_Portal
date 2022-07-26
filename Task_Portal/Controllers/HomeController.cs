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

        public IActionResult Dashboard()
        {
            List<TransactionRecordViewModel> transactionRecordsList = new List<TransactionRecordViewModel>();
            var customerRecords = _db.CustomerRecords.ToList();
            foreach (var item in customerRecords)
            {
                TransactionRecordViewModel recordViewModel = new TransactionRecordViewModel();
                recordViewModel.Id = item.Id;
                recordViewModel.CustName = item.CustName;
                recordViewModel.CustCode = item.CustCode;
                recordViewModel.Balance = item.Balance;
                recordViewModel.TransactionRecords = _db.TransactionRecords.Where(x=> x.CustCode == item.CustCode).ToList();
                if(recordViewModel.TransactionRecords?.Count > 0)
                {
                    var debitAmt = recordViewModel.TransactionRecords.Sum(x => x.DebitAmt);
                    var creditAmt = recordViewModel.TransactionRecords.Sum(x => x.CreditAmt);

                    if(recordViewModel.Balance > 0)
                    {
                        var creditbalance = creditAmt - debitAmt;
                        recordViewModel.TotalAmount = (recordViewModel.Balance + creditbalance).ToString();
                        recordViewModel.IsTotal = true;
                    }
                }
                transactionRecordsList.Add(recordViewModel);
            }
            return View(transactionRecordsList);
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