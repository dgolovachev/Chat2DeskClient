using System;
using System.Reflection;
using Chat2Desk.Attributes;

namespace Chat2Desk.Utils
{
    /// <summary>
    /// Расширяет Enum
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Получает строковое значение у перечисления, работает только если у перечисления присвоен атрибут StringValue
        /// </summary>
        /// <param name="value">значение</param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}