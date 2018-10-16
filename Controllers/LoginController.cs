using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeberPage.Models;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

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
                //grab the actual model object that we are working with, account
                //is a local copy that we made and not the real user object
                Account dbAccount = _weberpagecontext.Account.Where(o => o.Username == account.Username
                                                        && o.Password == account.Password).SingleOrDefault();

                if(dbAccount != null)
                {
                    //Debug.WriteLine($"LoginController ID: {dbAccount.Id} Username: {dbAccount.Username}");

                    //pass over the account ID to the new page
                    return RedirectToAction("Index", "WeberPageHome", new { id = dbAccount.Id });
                }
            }

            return View("Index", account);
        }

        public IActionResult CreateAccount()
        {
            return RedirectToAction("Index", "CreateAccount"); 
        }
    }
}
