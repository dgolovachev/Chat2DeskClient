using System;

namespace Chat2Desk.Attributes
{
    /// <summary>
    /// Аттрибут для перечисления
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Содержит строковое значение для значения в перечислении.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }
}
