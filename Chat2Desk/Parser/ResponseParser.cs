using System;
using Chat2Desk.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chat2Desk.Parser
{
    /// <summary>
    /// Парсит ответы от сервиса
    /// </summary>
    /// <seealso cref="Chat2Desk.Parser.IResponseParser" />
    public class ResponseParser : IResponseParser
    {
        /// <summary>
        /// Парсит json и приводи к типу Т
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">json is undefined.</exception>
        /// <exception cref="Chat2Desk.Exceptions.ParseException"></exception>
        public T Parse<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentException("json is undefined.");
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw new ParseException($"Parsing error: {e.Message}", e);
            }
            }

        /// <summary>
        /// Парсит json и приводи к типу Т определенную часть
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <param name="selectToken">The select token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// json is undefined.
        /// or
        /// selectToken is undefined.
        /// </exception>
        /// <exception cref="Chat2Desk.Exceptions.ParseException"></exception>
        public T ParseSelectToken<T>(string json, string selectToken)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentException("json is undefined.");
            if (string.IsNullOrEmpty(selectToken)) throw new ArgumentException("selectToken is undefined.");

            try
            {
                return JObject.Parse(json).SelectToken(selectToken, false).ToObject<T>();
            }
            catch (Exception e)
            {
                throw new ParseException($"Parsing error: {e.Message},\n json: {json},\n selectToken: {selectToken}", e);
            }
        }
    }
}