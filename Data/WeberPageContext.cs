using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeberPage.Models;

namespace WeberPage.Models
{
    public class WeberPageContext : DbContext
    {
        public WeberPageContext (DbContextOptions<WeberPageContext> options)
            : base(options)
        {
        }

        public DbSet<WeberPage.Models.Account> Account { get; set; }

        public DbSet<WeberPage.Models.Shift> Shift { get; set; }
    }
}
