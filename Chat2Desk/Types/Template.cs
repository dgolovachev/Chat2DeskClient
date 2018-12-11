using System;
using Chat2Desk.Converters;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Шаблон
    /// </summary>
    [JsonObject]
    public class Template
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Команда
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }
        /// <summary>
        /// Текст
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime Created { get; set; }
    }
}