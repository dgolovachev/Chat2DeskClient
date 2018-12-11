using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Обертка над результатом запроса
    /// </summary>
    [JsonObject]
    public class Response<T> where T : class
    {
        /// <summary>
        /// Данные
        /// </summary>
        [JsonProperty("data")]
        public T[] Data { get; set; }
        /// <summary>
        /// Дополнительная информация по выборке
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }
    }
}
