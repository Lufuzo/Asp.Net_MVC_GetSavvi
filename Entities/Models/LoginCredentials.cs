using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class LoginCredentials
    {
        [Key]
        public int loginId { get; set; }
        [Required(ErrorMessage = "Please enter username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }

       // public int FailedLoginAttempts { get; set; }
    }
}
