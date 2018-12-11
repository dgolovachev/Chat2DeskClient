using System;
using Chat2Desk.Types.Enums;
using Chat2Desk.Utils;
using Newtonsoft.Json;

namespace Chat2Desk.Converters
{
    /// <summary>
    /// TransportConverter
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class TransportConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageFrom = (Transport)value;
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
        /// <exception cref="NotImplementedException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(value))
            {
                switch (value)
                {
                    case "whatsapp": return Transport.Whatsapp;
                    case "viber": return Transport.Viber;
                    case "viber_business": return Transport.ViberBusiness;
                    case "viber_public": return Transport.ViberPublic;
                    case "telegram": return Transport.Telegram;
                    case "sms": return Transport.Sms;
                    case "facebook": return Transport.Facebook;
                    case "vk": return Transport.Vk;
                    case "external": return Transport.External;
                    case "widget": return Transport.Widget;
                    case "system": return Transport.System;
                    case "insta_direct": return Transport.InstaDirect;
                    case "ok": return Transport.OK;
                    case "skype": return Transport.Skype;
                    case "email": return Transport.Email;
                    case "insta_i2crm": return Transport.InstaI2Crm;
                    case "wechat": return Transport.Wechat;
                   
                    default: return Transport.Undefined;
                }
            }
            return Transport.Undefined;
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
