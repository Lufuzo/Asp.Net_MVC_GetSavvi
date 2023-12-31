﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Asp.Net_MVC_GetSavvi.Models
{
    public class UserModel
    {
        [Key]
        public int usersId { get; set; }

        [Required(ErrorMessage = "Please enter number.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "Phone must have 10.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter valid id")]
        [StringLength(13, MinimumLength = 0)]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public int loginId { get; set; }
       
    }
}