using System.Runtime.Serialization;
using Chat2Desk.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Роли
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        /// <summary>
        /// Пользователь не подтвердил свой e-mail. 
        /// </summary>
        [StringValue("unconfirmed")]
        [EnumMember(Value = "unconfirmed")]
        Unconfirmed,
        /// <summary>
        /// Оператор.
        /// </summary>
        [StringValue("operator")]
        [EnumMember(Value = "operator")]
        Operator,
        /// <summary>
        /// Оператор с расширенными правами (супервайзер).
        /// </summary>
        [StringValue("supervisor")]
        [EnumMember(Value = "supervisor")]
        Supervisor,
        /// <summary>
        /// Администратор.
        /// </summary>
        [StringValue("admin")]
        [EnumMember(Value = "admin")]
        Admin,
        /// <summary>
        ///  Заблокированный оператор.
        /// </summary>
        [StringValue("disabled")]
        [EnumMember(Value = "disabled")]
        Disabled,
        /// <summary>
        /// Удалённый оператор.
        /// </summary>
        [StringValue("deleted")]
        [EnumMember(Value = "deleted")]
        Deleted
    }
}