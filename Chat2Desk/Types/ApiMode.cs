using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject]
    public class ApiMode
    {
        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}