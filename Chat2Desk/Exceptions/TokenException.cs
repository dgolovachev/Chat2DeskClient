using System;

namespace Chat2Desk.Exceptions
{
    /// <summary>
    /// Ошибка токена
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class TokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenException"/> class.
        /// </summary>
        public TokenException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TokenException(String message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenException"/> class.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, указывающее причину создания исключения.</param>
        /// <param name="innerException">Исключение, вызвавшее текущее исключение, или пустая ссылка (Nothing в Visual Basic), если внутреннее исключение не задано.</param>
        public TokenException(String message, Exception innerException) : base(message, innerException) { }
    }
}