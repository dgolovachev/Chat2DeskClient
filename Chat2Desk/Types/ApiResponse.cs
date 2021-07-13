using Chat2Desk.Types.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Обертка над результатом запроса
    /// </summary>
    [JsonObject]
    public class ApiResponse<T> where T : class
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
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
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        [JsonProperty("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }
        /// <summary>
        /// Дополнительная информация по запросу
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}