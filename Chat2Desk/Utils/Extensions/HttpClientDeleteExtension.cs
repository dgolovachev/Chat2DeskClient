using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat2Desk.Utils.Extensions
{
    /// <summary>
    /// Расширяющий класс для HttpClient
    /// </summary>
    public static class HttpClientDeleteExtension
    {
        /// <summary>
        /// Позволяет передавать контент в Delete запрос
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url, HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };
            return await httpClient.SendAsync(request);
        }
    }
}