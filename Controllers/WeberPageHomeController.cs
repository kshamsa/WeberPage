using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeberPage.Controllers
{
    public class WeberPageHomeController : Controller
    {
        public IActionResult Index()
        {
            //retrun the IndexView by default
            return View();
        }
    }
}
