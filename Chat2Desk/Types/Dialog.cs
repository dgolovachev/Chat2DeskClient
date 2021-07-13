using System;
using Chat2Desk.Converters;
using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Диалог
    /// </summary>
    [JsonObject]
    public class Dialog
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Состояние диалога
        /// </summary>
        [JsonProperty("state")]
        public DialogState State { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        [JsonProperty("begin")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime Begin { get; set; }
        /// <summary>
        /// Дата закрытия
        /// </summary>
        [JsonProperty("end")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime End { get; set; }
        /// <summary>
        /// Id оператора
        /// </summary>
        [JsonProperty("operator_id")]
        public int? OperatorId { get; set; }
        /// <summary>
        /// Последнее сообщение
        /// </summary>
        [JsonProperty("last_message")]
        public DialogLastMessage LastMessage { get; set; }
        /// <summary>
        /// Колличество сообщений в диалоге
        /// </summary>
        [JsonProperty("messages")]
        public int? Messages { get; set; }
    }
}