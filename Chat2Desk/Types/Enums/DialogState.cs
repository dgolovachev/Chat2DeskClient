using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Состояние диалогов
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DialogState
    {
        /// <summary>
        /// Открыт
        /// </summary>
        [StringValue("open")]
        [EnumMember(Value = "open")]
        Open,
        /// <summary>
        /// Закрыт
        /// </summary>
        [StringValue("closed")]
        [EnumMember(Value = "closed")]
        Closed
    }
}