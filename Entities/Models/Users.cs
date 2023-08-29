﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Entities.Models
{
    public class Users
    {

        [Key]
        public int usersId { get; set; }

        [Required(ErrorMessage = "Please enter number.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        //[Display(Name = "Home Phone")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter valid id")]
        [StringLength(11, MinimumLength = 0)] 
        public string IdNumber { get; set; }

        [Required(ErrorMessage = " Please enter Email.")]
         [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public int loginId { get; set; }



    }
}
