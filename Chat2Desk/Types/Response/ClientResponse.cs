using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Response
{
    /// <summary>
    /// ClientResponse
    /// </summary>
    [JsonObject]
    public class ClientResponse
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public ClientData Data { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }
    }

    /// <summary>
    /// Data
    /// </summary>
    [JsonObject]
    public class ClientData
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// AssignedName
        /// </summary>
        [JsonProperty("assigned_name")]
        public string AssignedName { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
        /// <summary>
        /// ClientPhone
        /// </summary>
        [JsonProperty("client_phone")]
        public string ClientPhone { get; set; }
        /// <summary>
        /// RegionId
        /// </summary>
        [JsonProperty("region_id")]
        public int? RegionId { get; set; }
        /// <summary>
        /// CountryId
        /// </summary>
        [JsonProperty("country_id")]
        public int? CountryId { get; set; }
        /// <summary>
        /// ExternalId
        /// </summary>
        [JsonProperty("external_id")]
        public object ExternalId { get; set; }
        /// <summary>
        /// ExternalId
        /// </summary>
        [JsonProperty("external_ids")]
        public object ExternalIds { get; set; }
        /// <summary>
        /// ExtraComment1
        /// </summary>
        [JsonProperty("extra_comment_1")]
        public object ExtraComment1 { get; set; }
        /// <summary>
        /// ExtraComment2
        /// </summary>
        [JsonProperty("extra_comment_2")]
        public object ExtraComment2 { get; set; }
        /// <summary>
        /// CustomFields
        /// </summary>
        [JsonProperty("custom_fields")]
        public object CustomFields { get; set; }
        /// <summary>
        /// ExtraComment3
        /// </summary>
        [JsonProperty("extra_comment_3")]
        public object ExtraComment3 { get; set; }
        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
    }

}
