using Chat2Desk.Types.Enums;
using Newtonsoft.Json; 

namespace Chat2Desk.Types
{
    /// <summary>
    /// MessageResponse
    /// </summary>
    [JsonObject]
    public class MessageResponse
    {
        /// <summary>
        /// Id Сообщения
        /// </summary>
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
        /// <summary>
        /// Id канала
        /// </summary>
        [JsonProperty("channel_id")]
        public int ChannelId { get; set; }
        /// <summary>
        /// Id Оператора
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
        /// Id Клиента
        /// </summary>
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
        /// <summary>
        /// Id Диалога
        /// </summary>
        [JsonProperty("dialog_id")]
        public int DialogId { get; set; }
        /// <summary>
        /// Id Запроса
        /// </summary>
        [JsonProperty("request_id")]
        public int? RequestId { get; set; }
    }
}
