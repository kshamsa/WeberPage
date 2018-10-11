using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeberPage.Models;

namespace WeberPage.Controllers
{
    public class LoginController : Controller
    {
        private WeberPageContext _weberpagecontext;

        public LoginController(WeberPageContext wpc)
        {
            _weberpagecontext = wpc;
        }

        public IActionResult Index()
        {
            //retrun the IndexView by default
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index", account);
            }

            if (String.IsNullOrWhiteSpace(account.Username) == false &&
               String.IsNullOrWhiteSpace(account.Password) == false)
            {
                Account dbAccount = _weberpagecontext.Account.Where(o => o.Username == account.Username && o.Password == account.Password).SingleOrDefault();
            }

            int debug;

            return RedirectToAction("Index", "WeberPageHome");
        }
    }
}
