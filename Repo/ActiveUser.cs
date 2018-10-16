using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeberPage.Models;

namespace WeberPage.Repo
{
    public class ActiveUser
    {
        public static ActiveUser This { get; set; } = new ActiveUser(); 
        public Account CurrentUser { get; set; }
    }
}
