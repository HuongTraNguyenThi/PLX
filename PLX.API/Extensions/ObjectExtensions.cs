using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PLX.API.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(value, settings);
        }
    }
}