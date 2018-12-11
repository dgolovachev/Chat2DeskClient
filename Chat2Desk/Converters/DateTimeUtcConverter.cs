using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Chat2Desk.Converters
{
    /// <summary>
    /// Конвертер даты UTC
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DateTimeUtcConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = (DateTime)value;
            writer.WriteValue(val.ToString("dd.MM.yyyy HH:mm:ss"));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    if (value.Contains("UTC"))
                        return DateTime.ParseExact(value.Replace("UTC", "").Trim(), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                    if (Regex.IsMatch(value, @"\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}"))
                    {
                        return DateTime.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    return DateTime.ParseExact(value, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return DateTime.Now;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType) => true;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite => true;
    }
}