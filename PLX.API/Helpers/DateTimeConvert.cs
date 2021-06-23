using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PLX.API.Extensions.Converters
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
    }
}