using System;
using Chat2Desk.Types.Enums;
using Chat2Desk.Utils;
using Newtonsoft.Json;

namespace Chat2Desk.Converters
{
    /// <summary>
    /// Конвертер источника сообщения
    /// </summary>
    public class MessageFromConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageFrom = (MessageFrom)value;
            var val = messageFrom.GetStringValue();

            writer.WriteValue(val);
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
            if (reader.Value == null) return MessageFrom.Undefined;
            var value = reader.Value.ToString();
            if (!string.IsNullOrWhiteSpace(value))
            {
                switch (value)
                {
                    case "system": return MessageFrom.System;
                    case "autoreply": return MessageFrom.Autoreply;
                    case "from_client": return MessageFrom.FromClient;
                    case "to_client": return MessageFrom.ToClient;
                    default: return MessageFrom.Undefined;
                }
            }
            return MessageFrom.Undefined;
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