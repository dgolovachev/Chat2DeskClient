using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Статус ответа
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseStatus
    {
        /// <summary>
        /// Успешно
        /// </summary>
        [StringValue("success")]
        [EnumMember(Value = "success")]
        Success,
        /// <summary>
        /// Ошибка
        /// </summary>
        [StringValue("error")]
        [EnumMember(Value = "error")]
        Error
    }
}