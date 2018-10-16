using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeberPage.Models.ViewModel
{
    public class AccountShift
    {

        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public string Username { get; set; }

        public List<Shift> Shifts { get; set; }

        public bool ClockedIn { get; set; }
        public DateTime DateTimeClockedIn { get; set; }
        public DateTime DateTimeClockedOut { get; set; }
    }
}
