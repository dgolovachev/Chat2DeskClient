using System;

namespace Chat2Desk.Exceptions
{
    /// <summary>
    /// Ошибка парсинга
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        public ParseException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ParseException(String message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, указывающее причину создания исключения.</param>
        /// <param name="innerException">Исключение, вызвавшее текущее исключение, или пустая ссылка (Nothing в Visual Basic), если внутреннее исключение не задано.</param>
        public ParseException(String message, Exception innerException) : base(message, innerException) { }
    }
}