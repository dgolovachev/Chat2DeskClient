using Chat2Desk.Attributes;
using System.Runtime.Serialization;

namespace Chat2Desk.Types.Enums
{
    /// <summary>
    /// Сотрировка списка с конца или с начала
    /// </summary>
    public enum Order
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [StringValue("asc")]
        [EnumMember(Value = "asc")]
        Asc,
        /// <summary>
        /// Descending
        /// </summary>
        [StringValue("desc")]
        [EnumMember(Value = "desc")]
        Desc
    }
}
