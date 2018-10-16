using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeberPage.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using WeberPage.Repo;
using WeberPage.Models.ViewModel;
using System.Data.Entity.SqlServer;

namespace WeberPage.Controllers
{
    public class WeberPageHomeController : Controller
    {
        private WeberPageContext _weberpagecontext;
        public DateTime startOfWeek;
        public int delta;
        public DateTime endOfWeek;

        public WeberPageHomeController(WeberPageContext wpc)
        {
            _weberpagecontext = wpc;
            startOfWeek = DateTime.Today;
            delta = DayOfWeek.Monday - startOfWeek.DayOfWeek;
            startOfWeek = startOfWeek.AddDays(delta);
            endOfWeek = startOfWeek.AddDays(7);
        }

        public IActionResult Index(string id)
        {
            //single or default more or less says grab a single copy 
            ActiveUser.This.CurrentUser = _weberpagecontext.Account.Where(obj => obj.Id == Convert.ToInt32(id)).SingleOrDefault();

            return View(ActiveUser.This.CurrentUser);
        }

        [HttpGet]
        public IActionResult ClockIn()
        {
            var today = DateTime.Now.Date;
            Shift shift = _weberpagecontext.Shift.Where(obj => obj.AccountId == ActiveUser.This.CurrentUser.Id)
                .OrderByDescending(obj => obj.DateTimeClockedIn).FirstOrDefault();

            startOfWeek = DateTime.Today;
            delta = DayOfWeek.Monday - startOfWeek.DayOfWeek;
            startOfWeek = startOfWeek.AddDays(delta);

            endOfWeek = startOfWeek.AddDays(7);

            if (shift == null)
            {
                shift = new Shift()
                {
                    Id = 0,
                    AccountId = ActiveUser.This.CurrentUser.Id,
                    ClockedIn = false
                };
            }

            AccountShift temp = new AccountShift()
            {
                UserId = ActiveUser.This.CurrentUser.Id,
                ShiftId = shift.Id,
                ClockedIn = shift.ClockedIn,
                Username = ActiveUser.This.CurrentUser.Username,
                Shifts = _weberpagecontext.Shift.Where(obj => obj.AccountId == ActiveUser.This.CurrentUser.Id && 
                obj.DateTimeClockedIn > startOfWeek && obj.DateTimeClockedOut < endOfWeek).ToList()
            };

            if (Debugger.IsAttached)
            {
                foreach (var item in temp.Shifts)
                {
                    Debug.WriteLine(item.DateTimeClockedIn + " " + item.DateTimeClockedOut);
                }
            }

            return View(temp);
        }

        [HttpPost]
        public IActionResult PerformClockIn(AccountShift accountshift)
        {
            _weberpagecontext.Shift.Add(new Shift()
            {
                AccountId = accountshift.UserId,
                DateTimeClockedIn = DateTime.Now,
                ClockedIn = true
            });
            _weberpagecontext.SaveChanges();

            accountshift.DateTimeClockedIn = DateTime.Now;
            accountshift.ShiftId = _weberpagecontext.Shift.Where(i => i.AccountId == accountshift.UserId)
                  .OrderByDescending(i => i.DateTimeClockedIn).First().Id;

            accountshift.ClockedIn = true;
            accountshift.Shifts = _weberpagecontext.Shift.Where(obj => obj.AccountId == ActiveUser.This.CurrentUser.Id &&
             obj.DateTimeClockedIn > startOfWeek && obj.DateTimeClockedOut < endOfWeek).ToList();

            _weberpagecontext.SaveChanges();

            return View("ClockIn", accountshift);
        }

        [HttpPost]
        public IActionResult PerformClockOut(AccountShift accountshift)
        {
            Shift temp = _weberpagecontext.Shift.Where(obj => obj.Id == accountshift.ShiftId).Single();

            temp.ClockedIn = false;
            temp.DateTimeClockedOut = DateTime.Now;

            _weberpagecontext.Shift.Update(temp);
                
            accountshift.DateTimeClockedOut = DateTime.Now;
            accountshift.ClockedIn = false;
            accountshift.Shifts = _weberpagecontext.Shift.Where(obj => obj.AccountId == ActiveUser.This.CurrentUser.Id &&
                 obj.DateTimeClockedIn > startOfWeek && obj.DateTimeClockedOut < endOfWeek).ToList();

            _weberpagecontext.SaveChanges();

            return View("ClockIn", accountshift);
        }
    }
}
