using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Тег
    /// </summary>
    [JsonObject]
    public class Tag
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Id группы тега
        /// </summary>
        [JsonProperty("group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// Имя группы
        /// </summary>
        [JsonProperty("group_name")]
        public string GroupName { get; set; }
        /// <summary>
        /// Лэйбл
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}