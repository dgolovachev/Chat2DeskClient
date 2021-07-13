using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Attachment
    /// </summary>
    [JsonObject]
    public class Attachment
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
