using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asp.Net_MVC_GetSavvi.Models
{
    public class LoginCredentialsModel
    {


        public int loginId { get; set; }
        [Required(ErrorMessage = "Please enter username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter username.")]
        public string Password { get; set; }

        #region additional fields
        public int FailedLoginAttempts { get; set; }
        public bool IsLooked { get; set; }

        #endregion
    }
}