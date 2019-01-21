using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Response
{
    /// <summary>
    /// Ответ сервиса
    /// </summary>
    [JsonObject]
    public class ApiResponse
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// Статус ответа
        /// </summary>
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }
    }
}