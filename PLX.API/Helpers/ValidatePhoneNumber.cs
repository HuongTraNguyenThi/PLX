using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PLX.API.Helpers
{
    public class ValidatePhoneNumber
    {
        public static bool IsValid(string phone)
        {
            Regex regex = new Regex(@"^[0-9]{10}$");
            bool isValid = regex.IsMatch(phone);
            if (!isValid)
                return false;
            return true;
        }

    }
}