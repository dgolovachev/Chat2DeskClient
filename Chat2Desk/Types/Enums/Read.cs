using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Chat2Desk.Converters;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Статус прочтения сообщения
    /// </summary>
    [JsonConverter(typeof(ReadConverter))]
    public enum Read
    {
        /// <summary>
        /// Неопределенно
        /// </summary>
        Undefined,
        /// <summary>
        /// Непрочитанно
        /// </summary>
        [StringValue("0")]
        [EnumMember(Value = "0")]
        NotRead,
        /// <summary>
        /// Прочитано
        /// </summary>
        [StringValue("1")]
        [EnumMember(Value = "1")]
        Read
    }
}