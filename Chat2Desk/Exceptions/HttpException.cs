using System;

namespace Chat2Desk.Exceptions
{
    /// <summary>
    /// Ошибка HTTP 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class HttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        public HttpException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку.</param>
        public HttpException(String message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, указывающее причину создания исключения.</param>
        /// <param name="innerException">Исключение, вызвавшее текущее исключение, или пустая ссылка, если внутреннее исключение не задано.</param>
        public HttpException(String message, Exception innerException) : base(message, innerException) { }

    }
}