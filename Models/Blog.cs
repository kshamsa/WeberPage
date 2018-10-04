using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeberPage.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public int AccountId { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
