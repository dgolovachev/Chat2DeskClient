using System;

namespace Chat2Desk.Exceptions
{
    /// <summary>
    /// Превышен лимит запросов к API
    /// </summary>
    /// /// <seealso cref="System.Exception" />
    class APIExceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIExceededException"/> class.
        /// </summary>
        public APIExceededException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="APIExceededException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public APIExceededException(String message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="APIExceededException"/> class.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, указывающее причину создания исключения.</param>
        /// <param name="innerException">Исключение, вызвавшее текущее исключение, или пустая ссылка (Nothing в Visual Basic), если внутреннее исключение не задано.</param>
        public APIExceededException(String message, Exception innerException) : base(message, innerException) { }
    }
}
