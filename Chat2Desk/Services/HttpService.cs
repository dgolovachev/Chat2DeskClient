using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Chat2Desk.Exceptions;
using Chat2Desk.Utils.Extensions;
using Newtonsoft.Json;
using Method = Chat2Desk.Types.Enums.Method;

namespace Chat2Desk.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="webProxy">The web proxy.</param>
        /// <exception cref="System.ArgumentException">token can`t be empty</exception>
        public HttpService(string token, IWebProxy webProxy = null)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("token can`t be empty");
            _token = token;

            if (webProxy != null)
            {
                var httpClientHander = new HttpClientHandler
                {
                    Proxy = webProxy,
                    UseProxy = true
                };

                _httpClient = new HttpClient(httpClientHander);
            }
            else
            {
                _httpClient = new HttpClient();
            }
            
            _httpClient.DefaultRequestHeaders.Add("Authorization", _token);
        }

        /// <summary>
        /// Requests the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">url can`t be empty</exception>
        /// <exception cref="System.Exception">Unsoporte HTTP Method</exception>
        /// <exception cref="HttpException">Ошибка отправки запроса в HttpService</exception>
        public string Request(string url, Method method, object param = null)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("url can`t be empty");

            try
            {
                switch (method)
                {
                    case Method.GET:
                        {
                            //throw new Exception(_httpClient.DefaultRequestHeaders);
                            var response = _httpClient.GetAsync(url).Result;
                            return response.Content.ReadAsStringAsync().Result;
                        }
                    case Method.POST:
                        {
                            if (param == null) throw new ArgumentException("param for post request is required");
                            var data = JsonConvert.SerializeObject(param);
                            var content = new StringContent(data, Encoding.UTF8, "application/json");
                            var response = _httpClient.PostAsync(url, content).Result;
                            return response.Content.ReadAsStringAsync().Result;
                        }
                    case Method.PUT:
                        {
                            if (param == null) throw new ArgumentException("param for put request is required");
                            var data = JsonConvert.SerializeObject(param);
                            var content = new StringContent(data, Encoding.UTF8, "application/json");
                            var response = _httpClient.PutAsync(url, content).Result;
                            return response.Content.ReadAsStringAsync().Result;
                        }
                    case Method.DELETE:
                        {
                            HttpResponseMessage response;
                            if (param != null)
                            {
                                var data = JsonConvert.SerializeObject(param);
                                var content = new StringContent(data, Encoding.UTF8, "application/json");
                                response = _httpClient.DeleteAsync(url, content).Result;
                            }
                            else
                                response = _httpClient.DeleteAsync(url).Result;
                            return response.Content.ReadAsStringAsync().Result;
                        }
                    default: throw new Exception("Unsupported HTTP Method");
                }

            }
            catch (Exception e)
            {
                throw new HttpException("Ошибка отправки запроса в HttpService", e);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}