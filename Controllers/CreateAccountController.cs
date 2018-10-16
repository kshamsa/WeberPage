using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeberPage.Models;

namespace WeberPage.Controllers
{
    public class CreateAccountController : Controller
    {
        //creating a datacontext for where my data is
        private WeberPageContext _weberpagecontext;

        //having data passed in that will be assigned to datacontext
        public CreateAccountController(WeberPageContext wpc)
        {
            //datacontext assigned
            _weberpagecontext = wpc;
        }

        public IActionResult Index()
        {
            //retrun the IndexView by default
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(Account account)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index", account);
            }

            if (String.IsNullOrWhiteSpace(account.Username) == false &&
                String.IsNullOrWhiteSpace(account.Password) == false)
            {
                //go to the "database" and return whatever value is equal to the username passed in
                //if that username exists, it will return that username, if it does not it will return NULL
                Account fromDB = _weberpagecontext.Account.FirstOrDefault(ac => ac.Username == account.Username);

                if (fromDB == null)
                {

                    _weberpagecontext.Account.Add(account);
                    _weberpagecontext.SaveChanges();

                    //Now that the account exists, grab it
                    Account dbAccount = _weberpagecontext.Account.Where(o => o.Username == account.Username
                                                      && o.Password == account.Password).SingleOrDefault();

                    //call the index method from the WeberHomePage controller
                    return RedirectToAction("Index", "WeberPageHome", new { id = dbAccount.Id });
                }
                else
                {
                    // Acount DOES exist.
                    ModelState.AddModelError("", "This account already exists.  Please select a different name");
                    return View("Index", account);
                }

            }
            else
            {
                ModelState.AddModelError("", "Emtpy Username and Password");
                return View("Index", account);
            }
        }

        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");//, new { id = account.Id });
        }
    }
}