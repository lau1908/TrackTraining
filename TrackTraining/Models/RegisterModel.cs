using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrackTraining.Models
{
    public class RegisterModel //klasse til at registere ny bruger
    {
        [Required]
        public string Username { get; set; } //et felt til brugernavn

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } //et felt til emailen

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }//et felt til adgangskoden
    }
}