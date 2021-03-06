using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using PLX.API.Data.DTO.LinkedCard;
using PLX.API.Data.DTO.Vehicle;

namespace PLX.API.Helpers
{
    public static class Validation
    {
        public static bool IsValidPhone(string phone)
        {
            Regex regex = new Regex(@"^[0-9]{10,12}$");
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
        public static bool IsEqualOrLessThanZero(int x)
        {
            return x <= 0;
        }
        public static bool Equals(string a, string b)
        {
            return string.Equals(a, b);
        }
        public static bool CheckNullOrEmptyLinkedCard(LinkedCardRequest linkedCard)
        {
            if (Validation.IsNullOrEmpty(linkedCard.Name) && !Validation.IsNullOrEmpty(linkedCard.CardNumber))
                return false;
            return true;
        }

        public static bool CheckNullOrEmptyVehicle(VehicleRequest vehicle)
        {
            if (Validation.IsNullOrEmpty(vehicle.Name) && !Validation.IsNullOrEmpty(vehicle.LicensePlate) && !Validation.IsEqualOrLessThanZero(vehicle.VehicleTypeId))
                return false;
            return true;
        }

    }
}