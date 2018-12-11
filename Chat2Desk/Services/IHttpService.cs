using Chat2Desk.Types.Enums;

namespace Chat2Desk.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Requests the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        string Request(string url, Method method, object param = null);
    }
}