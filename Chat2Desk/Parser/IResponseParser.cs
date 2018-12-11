namespace Chat2Desk.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponseParser
    {
        /// <summary>
        /// Парсит json и приводи к типу Т
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        T Parse<T>(string json);
        /// <summary>
        /// Парсит json и приводи к типу Т определенную часть
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <param name="selectToken">The select token.</param>
        /// <returns></returns>
        T ParseSelectToken<T>(string json, string selectToken);
    }
}