using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Chat2Desk.Converters;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Тип сообщение (от кого сообщение)
    /// </summary>
    [JsonConverter(typeof(MessageFromConverter))]
    public enum MessageFrom
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        [StringValue("undefined")]
        Undefined,
        /// <summary>
        /// Системное
        /// </summary>
        [StringValue("system")]
        [EnumMember(Value = "system")]
        System,
        /// <summary>
        /// Автоответ
        /// </summary>
        [StringValue("autoreply")]
        [EnumMember(Value = "autoreply")]
        Autoreply,
        /// <summary>
        /// Клиенту
        /// </summary>
        [StringValue("to_client")]
        [EnumMember(Value = "to_client")]
        ToClient,
        /// <summary>
        /// От клиента
        /// </summary>
        [StringValue("from_client")]
        [EnumMember(Value = "from_client")]
        FromClient
    }
}