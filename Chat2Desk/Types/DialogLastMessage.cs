using System;
using Chat2Desk.Converters;
using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Последнее сообщение в диалоге
    /// </summary>
    [JsonObject]
    public class DialogLastMessage
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Текст
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        /// Тип сообщение (от кого сообщение)
        /// </summary>
        [JsonProperty("type")]
        public MessageFrom Type { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime Created { get; set; }
    }
}