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
        Wechat,
        /// <summary>
        /// Wa_Clickatell
        /// </summary>
        [StringValue("wa_clickatell")]
        [EnumMember(Value = "wa_clickatell")]
        Wa_Clickatell,
        /// <summary>
        /// Viber_Tr
        /// </summary>
        [StringValue("viber_tr")]
        [EnumMember(Value = "viber_tr")]
        Viber_Tr,
        /// <summary>
        /// Yandex_Dialogs
        /// </summary>
        [StringValue("yandex_dialogs")]
        [EnumMember(Value = "yandex_dialogs")]
        Yandex_Dialogs,
        /// <summary>
        /// Wa_Infobip
        /// </summary>
        [StringValue("wa_infobip")]
        [EnumMember(Value = "wa_infobip")]
        Wa_Infobip,
        /// <summary>
        /// Wa_Cm
        /// </summary>
        [StringValue("wa_cm")]
        [EnumMember(Value = "wa_cm")]
        Wa_Cm,
        /// <summary>
        /// Wa_Tr
        /// </summary>
        [StringValue("wa_tr")]
        [EnumMember(Value = "wa_tr")]
        Wa_Tr,
        /// <summary>
        /// Wa_Wavy
        /// </summary>
        [StringValue("wa_wavy")]
        [EnumMember(Value = "wa_wavy")]
        Wa_Wavy,
        /// <summary>
        /// Wa_Botmaker
        /// </summary>
        [StringValue("wa_botmaker")]
        [EnumMember(Value = "wa_botmaker")]
        Wa_Botmaker,
        /// <summary>
        /// Viber_Infobip
        /// </summary>
        [StringValue("viber_infobip")]
        [EnumMember(Value = "viber_infobip")]
        Viber_Infobip,
        /// <summary>
        /// Wa_Dialog
        /// </summary>
        [StringValue("wa_dialog")]
        [EnumMember(Value = "wa_dialog")]
        Wa_Dialog,
        /// <summary>
        /// Insta_Local
        /// </summary>
        [StringValue("insta_local")]
        [EnumMember(Value = "insta_local")]
        Insta_Local,
        /// <summary>
        /// Tg_User
        /// </summary>
        [StringValue("tg_user")]
        [EnumMember(Value = "tg_user")]
        Tg_User,
        /// <summary>
        /// Vox_Implant
        /// </summary>
        [StringValue("vox_implant")]
        [EnumMember(Value = "vox_implant")]
        Vox_Implant,
        /// <summary>
        /// Sms_Infobip
        /// </summary>
        [StringValue("sms_infobip")]
        [EnumMember(Value = "sms_infobip")]
        Sms_Infobip,
    }
}