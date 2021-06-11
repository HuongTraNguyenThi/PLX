using System;
using System.Text.Json.Serialization;

namespace PLX.API.Extensions.Converters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateMMYYMMMMAttribute : JsonConverterAttribute
    {
        public DateMMYYMMMMAttribute() : base(typeof(JsonDateConverter))
        {

        }
    }
}