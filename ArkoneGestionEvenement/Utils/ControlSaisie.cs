using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.Utils
{
    public static class ControlSaisie
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }


        public static bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9]{6,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(username, pattern);
        }


        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{12,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(password, pattern);
        }
    }
}
