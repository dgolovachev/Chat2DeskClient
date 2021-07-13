using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Клиент
    /// </summary>
    [JsonObject]
    public class Client
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Аватар
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Присвоенное имя
        /// </summary>
        [JsonProperty("assigned_name")]
        public string AssignedName { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
        /// <summary>
        /// Id региона
        /// </summary>
        [JsonProperty("region_id")]
        public int? RegionId { get; set; }
        /// <summary>
        /// Id страны
        /// </summary>
        [JsonProperty("country_id")]
        public int? CountryId { get; set; }
        /// <summary>
        /// Дополнительный комментарий 1
        /// </summary>
        [JsonProperty("extra_comment_1")]
        public string ExtraComment1 { get; set; }
        /// <summary>
        /// Дополнительный комментарий 2
        /// </summary>
        [JsonProperty("extra_comment_2")]
        public string ExtraComment2 { get; set; }
        /// <summary>
        /// Дополнительный комментарий 3
        /// </summary>
        [JsonProperty("extra_comment_3")]
        public string ExtraComment3 { get; set; }
        /// <summary>
        /// Дополнительный идентификатор
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }
        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public List<Dictionary<string, string>> Tags { get; set; }
    }
}