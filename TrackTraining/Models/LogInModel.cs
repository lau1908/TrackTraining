using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackTraining.Models
{
    public class LogInModel //klassen til at logge ind
    {
        //der bliver lavet en forms udfra denne klasse
        [Required]
        public string Username { get; set; }  //der skal kunne skrives et brugernavn ind

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } //der skal kunne skrives en adgangskode ind

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; } //ReturnUrl er gemt, så brugeren skal ikke kunne tilgå denne, den bliver brugt senere hen i authcontroller
    }
}