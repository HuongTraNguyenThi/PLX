using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PLX.API.Helpers
{
    public class DateTimeConvert
    {
        public static DateTime ToDateTime(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ToString(DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string DateToString(DateTime value)
        {
            return value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static DateTime ToDate(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}