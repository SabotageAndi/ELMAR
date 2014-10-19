using System;
using Newtonsoft.Json.Linq;

namespace net.the_engineers.elmar.everywhere.net45
{
    public static class JsonObjectExtension
    {
        public static T GetValue<T>(this JObject jsonObject, string propertyName, StringComparison stringComparison, T defaultValue)
        {
            JToken jtoken;
            if (!jsonObject.TryGetValue(propertyName, stringComparison, out jtoken))
                return defaultValue;

            return jtoken.Value<T>();
        }

        public static T GetValue<T>(this JObject jsonObject, string propertyName, T defaultValue)
        {
            JToken jtoken;
            if (!jsonObject.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out jtoken))
                return defaultValue;

            return jtoken.Value<T>();
        }
    }
}