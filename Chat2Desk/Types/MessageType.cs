using Chat2Desk.Attributes;
using System.Runtime.Serialization;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Тип сообщение (для отправки сообщения)
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Клиенту
        /// </summary>
        [StringValue("to_client")]
        [EnumMember(Value = "to_client")]
        ToClient,
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
        /// внутренний текстовый комментарий
        /// </summary>
        [StringValue("comment")]
        [EnumMember(Value = "comment")]
        Comment
    }
}
