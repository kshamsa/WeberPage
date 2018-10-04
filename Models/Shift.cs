using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeberPage.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public bool ClockedIn { get; set; }
        public DateTime DateTimeClockedIn { get; set; }
        public DateTime DateTimeClockedOut { get; set; }
    }
}
