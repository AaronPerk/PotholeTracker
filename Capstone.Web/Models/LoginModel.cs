using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Capstone.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "An email is required")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        public static bool ComparePasswords(string plainTextPassword, string existingHashedPassword, string salt, int workFactor)
        {
            Rfc2898DeriveBytes cryptoProvider = new Rfc2898DeriveBytes(plainTextPassword, Convert.FromBase64String(salt), workFactor);

            byte[] hash = cryptoProvider.GetBytes(20);

            string newHashedPassword = Convert.ToBase64String(hash);

            return (newHashedPassword == existingHashedPassword);
        }
    }
}