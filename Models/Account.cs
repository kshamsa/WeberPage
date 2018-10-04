using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeberPage.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4, ErrorMessage ="Username must be at least 4 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
