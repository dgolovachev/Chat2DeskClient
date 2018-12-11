using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Chat2Desk.Converters;
using Newtonsoft.Json;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Транспорты
    /// </summary>
    [JsonConverter(typeof(TransportConverter))]
    public enum Transport
    {
        /// <summary>
        /// Неопределен
        /// </summary>
        [StringValue("-")]
        [EnumMember(Value = "-")]
        Undefined,
        /// <summary>
        /// Whatsapp
        /// </summary>
        [StringValue("whatsapp")]
        [EnumMember(Value = "whatsapp")]
        Whatsapp,
        /// <summary>
        /// Viber
        /// </summary>
        [StringValue("viber")]
        [EnumMember(Value = "viber")]
        Viber,
        /// <summary>
        /// Viber - Business
        /// </summary>
        [StringValue("viber_business")]
        [EnumMember(Value = "viber_business")]
        ViberBusiness,
        /// <summary>
        /// Viber - Public
        /// </summary>
        [StringValue("viber_public")]
        [EnumMember(Value = "viber_public")]
        ViberPublic,
        /// <summary>
        /// Telegram
        /// </summary>
        [StringValue("telegram")]
        [EnumMember(Value = "telegram")]
        Telegram,
        /// <summary>
        /// SMS
        /// </summary>
        [StringValue("sms")]
        [EnumMember(Value = "sms")]
        Sms,
        /// <summary>
        /// Facebook
        /// </summary>
        [StringValue("facebook")]
        [EnumMember(Value = "facebook")]
        Facebook,
        /// <summary>
        /// VK
        /// </summary>
        [StringValue("vk")]
        [EnumMember(Value = "vk")]
        Vk,
        /// <summary>
        /// External
        /// </summary>
        [StringValue("external")]
        [EnumMember(Value = "external")]
        External,
        /// <summary>
        /// Widget
        /// </summary>
        [StringValue("widget")]
        [EnumMember(Value = "widget")]
        Widget,
        /// <summary>
        /// System
        /// </summary>
        [StringValue("system")]
        [EnumMember(Value = "system")]
        System,
        /// <summary>
        /// InstaDirect
        /// </summary>
        [StringValue("insta_direct")]
        [EnumMember(Value = "insta_direct")]
        InstaDirect,
        /// <summary>
        /// OK
        /// </summary>
        [StringValue("ok")]
        [EnumMember(Value = "ok")]
        OK,
        /// <summary>
        /// Skype
        /// </summary>
        [StringValue("skype")]
        [EnumMember(Value = "skype")]
        Skype,
        /// <summary>
        /// Email
        /// </summary>
        [StringValue("email")]
        [EnumMember(Value = "email")]
        Email,
        /// <summary>
        /// Insta_i2crm
        /// </summary>
        [StringValue("insta_i2crm")]
        [EnumMember(Value = "insta_i2crm")]
        InstaI2Crm,
        /// <summary>
        /// Wechat
        /// </summary>
        [StringValue("wechat")]
        [EnumMember(Value = "wechat")]
        Wechat
    }
}