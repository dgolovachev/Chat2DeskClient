using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Транспорты клиента
    /// </summary>
    [JsonObject]
    public class ClientTransports
    {
        /// <summary>
        /// Id канала
        /// </summary>
        [JsonProperty("channel_id")]
        public int ChannelId { get; set; }
        /// <summary>
        /// Транспорты
        /// </summary>
        [JsonProperty("transports")]
        public Transport[] Transports { get; set; }
    }
}