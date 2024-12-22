using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotiblingBackend.Utilities.Validation
{
    public static partial class PasswordValidator
    {


        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s])[A-Za-z\d^\w\d\s\S]{8,}$";


        //public static bool IsValid(string password)
        //{
        //        var regex = new Regex(PasswordPattern);
        //        return regex.IsMatch(password);

        //}

        [GeneratedRegex(PasswordPattern)]
        public static partial Regex PasswordRegex();

        public static bool IsValid(string password)
        {
            return PasswordRegex().IsMatch(password);
        }

    }
}
