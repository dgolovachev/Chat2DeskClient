using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Дополнительная информация по выборке
    /// </summary>
    [JsonObject]
    public class Meta
    {
        /// <summary>
        /// Всего записей
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
        /// <summary>
        /// Лимит
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// Отступ
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}