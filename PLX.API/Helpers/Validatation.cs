using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PLX.API.Helpers
{
    public static class Validation
    {
        public static bool CheckPhone(string phone)
        {
            Regex regex = new Regex(@"^[0-9]{10}$");
            bool isValid = regex.IsMatch(phone ?? "");
            if (!isValid)
                return false;
            return true;
        }
        public static bool CheckDate(string date)
        {
            DateTime dt;
            bool isValid = DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (!isValid)
                return false;
            return true;
        }
        public static bool IsNullOrEmpty(string text)
        {
            if (text == "" || text == null)
                return false;
            return true;
        }
        public static bool IsNullOrEmpty(int x)
        {
            if (x == 0)
                return false;
            return true;
        }

    }
}