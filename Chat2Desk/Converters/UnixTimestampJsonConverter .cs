using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Chat2Desk.Converters
{
    /// <summary>
    /// Конвертер даты Unix
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class UnixTimestampJsonConverter : JsonConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            long ts = serializer.Deserialize<long>(reader);

            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(ts);
        }

        public override bool CanConvert(Type type)
        {
            return typeof(DateTime).IsAssignableFrom(type);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}