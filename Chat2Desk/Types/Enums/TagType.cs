using Chat2Desk.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Тип тега (Нужен для методов привзяки и удаления тега чтобы выбрать параметр привзяки и удаления)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TagType
    {
        /// <summary>
        /// Открыт
        /// </summary>
        [StringValue("client")]
        [EnumMember(Value = "client")]
        Client,
        /// <summary>
        /// Закрыт
        /// </summary>
        [StringValue("request")]
        [EnumMember(Value = "request")]
        Request
    }
}
