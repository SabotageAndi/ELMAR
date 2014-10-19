using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using net.the_engineers.elmar.everywhere;

namespace net.the_engineers.elmar.everywhere.net45
{
    public class TimeJsonConverter : CustomCreationConverter<Time>
    {
        public override Time Create(Type objectType)
        {
            return new Time();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Time);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var time = new Time();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var name = reader.Value.ToString().ToLower();
                    if (reader.Read())
                    {
                        var valueString = reader.Value.ToString();
                        int value = Convert.ToInt32(valueString);

                        switch (name)
                        {
                            case "hours":
                                time.Hours = value;
                                break;
                            case "minutes":
                                time.Minutes = value;
                                break;
                            case "seconds":
                                time.Seconds = value;
                                break;
                        }
                    }
                }
            }

            return time;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var time = (Time) value;

            writer.WriteStartObject();

            writer.WritePropertyName("hours");
            writer.WriteValue(time.Hours);

            writer.WritePropertyName("minutes");
            writer.WriteValue(time.Minutes);

            writer.WritePropertyName("seconds");
            writer.WriteValue(time.Seconds);

            writer.WriteEnd();
        }
    }
}