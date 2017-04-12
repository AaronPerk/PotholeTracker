using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Capstone.Web.Models
{
    public class RegisterModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "Passwords must have at least 8 characters, one uppercase, one lowercase, one number, and one special character")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password: ")]
        [Required(ErrorMessage = "This field is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        public string UserType { get; set; }

        public string Salt { get; set; }

        public static KeyValuePair<string, string> HashPassword(string password, int saltSize, int workFactor)
        {
            Rfc2898DeriveBytes cryptoProvider = new Rfc2898DeriveBytes(password, 8, 100000);

            byte[] hash = cryptoProvider.GetBytes(20);
            byte[] salt = cryptoProvider.Salt;

            KeyValuePair<string, string> saltAndHash = new KeyValuePair<string, string> ( Convert.ToBase64String(salt), Convert.ToBase64String(hash) );

            return saltAndHash;
        }

    }
}