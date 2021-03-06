﻿using System;
using System.Collections.Generic;
using Chat2Desk.Converters;
using Chat2Desk.Types.Enums;
using Newtonsoft.Json;

namespace Chat2Desk.Types
{
    /// <summary>
    /// Сообщение
    /// </summary>
    [JsonObject]
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Video
        /// </summary>
        [JsonProperty("video")]
        public object Video { get; set; }

        /// <summary>
        /// Photo
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; }

        /// <summary>
        /// Coordinates
        /// </summary>
        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }
        
        //[JsonProperty("transport")]
        //[JsonConverter(typeof(TransportConverter))]
        //public string Transport { get; set; }
        
        /// <summary>
        /// Транспорт
        /// </summary>
        [JsonProperty("transport")]
        public string Transport { get; set; }  

        /// <summary>
        /// Тип сообщение (от кого сообщение)
        /// </summary>
        //[JsonProperty("type")]
        //public MessageFrom Type { get; set; } 
        [JsonProperty("type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Статус прочтения сообщения
        /// </summary>
        //[JsonProperty("read")]
        //[JsonConverter(typeof(ReadConverter))]
        //public Read Read { get; set; } 
        [JsonProperty("read")]
        public string Read { get; set; } 
        
        /// <summary>
        /// Дата создание
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(DateTimeUtcConverter))]
        public DateTime Created { get; set; }
        
        /// <summary>
        /// Audio
        /// </summary>
        [JsonProperty("audio")]
        public object Audio { get; set; }
        
        /// <summary>
        /// Pdf
        /// </summary>
        [JsonProperty("pdf")]
        public string Pdf { get; set; }
        
        /// <summary>
        /// RemoteId
        /// </summary>
        [JsonProperty("remote_id")]
        public int? RemoteId { get; set; }
        
        /// <summary>
        /// RecipientStatus
        /// </summary>
        [JsonProperty("recipient_status")]
        public object RecipientStatus { get; set; }
        /// <summary>
        /// Id оператора
        /// </summary>
        [JsonProperty("operator_id")]
        public int? OperatorId { get; set; }
        /// <summary>
        /// Id канала
        /// </summary>
        [JsonProperty("channel_id")]
        public int? ChannelId { get; set; }
        /// <summary>
        /// Id диалога
        /// </summary>
        [JsonProperty("dialog_id")]
        public int? DialogId { get; set; }
        /// <summary>
        /// Id клиента
        /// </summary>
        [JsonProperty("client_id")]
        public int? ClientId { get; set; }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// IsNew
        /// </summary>
        [JsonProperty("is_new")]
        public int? IsNew { get; set; }

        /// <summary>
        /// AiTips
        /// </summary>
        [JsonProperty("ai_tips")]
        public object AiTips { get; set; }

        /// <summary>
        /// RequestId
        /// </summary>
        [JsonProperty("request_id")]
        public int? RequestId { get; set; }

        /// <summary>
        /// ExtraData
        /// </summary>
        [JsonProperty("extra_data")]
        public Dictionary<string, string> ExtraData { get; set; }
    }
}