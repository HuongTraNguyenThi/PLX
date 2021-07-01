using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PLX.API.Helpers
{
    public static class Validation
    {
        public static bool IsValidPhone(string phone)
        {
            Regex regex = new Regex(@"^[0-9]{10}$");
            bool isValid = regex.IsMatch(phone ?? "");
            return isValid;
        }
        public static bool IsValidDate(string date)
        {
            DateTime dt;
            bool isValid = DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            return isValid;
        }
        public static bool IsNullOrEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
        public static bool IsNonZero(int x)
        {
            return x != 0;
        }

    }
}