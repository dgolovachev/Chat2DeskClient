﻿using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Response
{
    /// <summary>
    /// MessageReponse
    /// </summary>
    [JsonObject]
    public class MessageReponse
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public MessageData Data { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }
    }
    /// <summary>
    /// Data
    /// </summary>
    [JsonObject]
    public class MessageData
    {
        /// <summary>
        /// ИД сообщения
        /// </summary>
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
        /// <summary>
        /// ИД канала
        /// </summary>
        [JsonProperty("channel_id")]
        public int ChannelId { get; set; }
        /// <summary>
        /// ИД Оператора
        /// </summary>
        [JsonProperty("operator_id")]
        public int OperatorId { get; set; }
        /// <summary>
        /// Транспорт
        /// </summary>
        [JsonProperty("transport")]
        public string Transport { get; set; }
        /// <summary>
        /// От кого сообщение
        /// </summary>
        [JsonProperty("type")]
        public MessageFrom Type { get; set; }
        /// <summary>
        /// ИД Клиента
        /// </summary>
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
        /// <summary>
        /// ИД Диалога
        /// </summary>
        [JsonProperty("dialog_id")]
        public int DialogId { get; set; }
        /// <summary>
        /// ИД Запроса
        /// </summary>
        [JsonProperty("request_id")]
        public int? RequestId { get; set; }
    }

}