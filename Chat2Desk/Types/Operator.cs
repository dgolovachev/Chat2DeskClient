using System;
using Chat2Desk.Converters;
using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Оператор
    /// </summary>
    [JsonObject]
    public class Operator
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        [JsonProperty("role")]
        public Role Role { get; set; }
        /// <summary>
        /// Online
        /// </summary>
        [JsonProperty("online")]
        public int Online { get; set; }
        /// <summary>
        /// OfflineType
        /// </summary>
        [JsonProperty("offline_type")]
        public object OfflineType { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Последний визит
        /// </summary>
        [JsonProperty("last_visit")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime LastVisit { get; set; }
        /// <summary>
        /// Количество открытых диалогов
        /// </summary>
        [JsonProperty("opened_dialogs")]
        public int OpenedDialogs { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        [JsonProperty("status_id")]
        public int? StatusId { get; set; }
        /// <summary>
        /// Внешний Идентификатор
        /// </summary>
        [JsonProperty("external_id")]
        public int? ExternalId { get; set; }
    }
}