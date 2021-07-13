using System;
using System.Collections.Generic;
using Chat2Desk.Converters; 
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
        public string Type { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime Created { get; set; }
        /// <summary>
        /// Гео координаты
        /// </summary>
        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }
        /// <summary>
        /// Траноспорт
        /// </summary>
        [JsonProperty("transport")]
        public string Transport { get; set; }
        /// <summary>
        /// Прочитанно
        /// </summary>
        [JsonProperty("read")]
        public string Read { get; set; }
        /// <summary>
        /// PDF
        /// </summary>
        [JsonProperty("pdf")]
        public string Pdf { get; set; }
        /// <summary>
        /// Удаленный id
        /// </summary>
        [JsonProperty("remote_id")]
        public int? RemoteId { get; set; }
        /// <summary>
        /// recipient_status
        /// </summary>
        [JsonProperty("recipient_status")]
        public object RecipientStatus { get; set; }
        /// <summary>
        /// Id диалога
        /// </summary>
        [JsonProperty("dialog_id")]
        public int? DialogId{ get; set; }
        /// <summary>
        /// Id Оператора
        /// </summary>
        [JsonProperty("operator_id")]
        public int? OperatorId { get; set; }
        /// <summary>
        /// Id кнаала
        /// </summary>
        [JsonProperty("channel_id")]
        public int? ChanenelId { get; set; }
        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
        /// <summary>
        /// AiTips
        /// </summary>
        [JsonProperty("ai_tips")]
        public object AiTips { get; set; }
        /// <summary>
        /// Audio
        /// </summary>
        [JsonProperty("audio")]
        public object Audio { get; set; }
        /// <summary>
        /// Video
        /// </summary>
        [JsonProperty("video")]
        public object Video { get; set; }
        /// <summary>
        /// Photo
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; }
        /// <summary>
        /// Id клиента
        /// </summary>
        [JsonProperty("client_id")]
        public int? ClientId { get; set; }
        /// <summary>
        /// Id запрса
        /// </summary>
        [JsonProperty("request_id")]
        public int? RequestId { get; set; }
        /// <summary>
        /// ExtraData
        /// </summary>
        [JsonProperty("extra_data")]
        public Dictionary<string, string> ExtraData { get; set; }
    }
}