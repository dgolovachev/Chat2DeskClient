using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Иформация по сервису
    /// </summary>
    [JsonObject]
    public class Info
    {
        /// <summary>
        /// Уровень доступа к API.
        /// </summary>
        [JsonProperty("mode")]
        public string Mode { get; set; }
        /// <summary>
        /// Url Web Hook
        /// </summary>
        [JsonProperty("hook")]
        public string Hook { get; set; }
        /// <summary>
        /// Url закрытия Web hook
        /// </summary>
        [JsonProperty("chat_close_web_hook")]
        public string ChatCloseWebHook { get; set; }
        /// <summary>
        /// Версия
        /// </summary>
        [JsonProperty("current_version")]
        public int CurrentVersion { get; set; }
        /// <summary>
        /// Количество запросов за месяц
        /// </summary>
        [JsonProperty("requests_this_month")]
        public int RequestsThisMonth { get; set; }
        /// <summary>
        /// Каналы
        /// </summary>
        [JsonProperty("channels")]
        public int Channels { get; set; }
        /// <summary>
        /// Название компании
        /// </summary>
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
    }
}