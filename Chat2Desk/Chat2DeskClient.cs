using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Chat2Desk.Exceptions;
using Chat2Desk.Parser;
using Chat2Desk.Services;
using Chat2Desk.Types;
using Chat2Desk.Types.Enums;
using Chat2Desk.Utils;
using Chat2Desk.Utils.Extensions;

namespace Chat2Desk
{
    /// <summary>
    /// Клиент для работы с сервисом chat2desk
    /// </summary>
    public class Chat2DeskClient
    {
        private readonly IHttpService _httpService;
        private readonly IResponseParser _responseParser;

        private const string BaseUrl = @"https://api.chat2desk.com/v1";
        private readonly string _clientsBaseUrl = $"{BaseUrl}/clients";
        private readonly string _dialogsBaseUrl = $"{BaseUrl}/dialogs";
        private readonly string _tagsBaseUrl = $"{BaseUrl}/tags";
        private readonly string _channelsBaseUrl = $"{BaseUrl}/channels";
        private readonly string _templatesBaseUrl = $"{BaseUrl}/templates";
        private readonly string _messagesBaseUrl = $"{BaseUrl}/messages";

        private const string AuthErrorMessage = "You need to auth";
        private const string ApiRequestExceededMessage = "Number of API requests exceeded";
        private const string ApiCallsExeededMessage = "API calls exceeded API limit per month";

        /// <summary>
        /// Проверяет на ошибки запросов к API
        /// </summary>
        /// <param name="response"></param>  
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        private void CheckResponse(string response)
        {
            if (response.Contains(AuthErrorMessage)) throw new TokenException("Ошибка токена, проверьте токен");
            if (response.Contains(ApiRequestExceededMessage)) throw new APIExceededException("Превышен лимит запросов к API");
            if (response.Contains(ApiCallsExeededMessage)) throw new APIExceededException("Превышен лимит запросов к API");
        }

        /// <summary>
        /// Установка web hook, допускается любой URL, но рекомендуется https. Если передаётся пустое значение (null), то web hook удаляется.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse WebHook(string url = null)
        {
            var webHookUrl = $"{BaseUrl}companies/web_hook";
            var response = _httpService.Request(webHookUrl, Method.POST, string.IsNullOrWhiteSpace(url) ? new { url = string.Empty } : new { url = url });
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        /// <summary>
        /// Возвращает: 
        ///• Текущая версия API, используемая сервисом Chat Helpdesk.
        ///• Ваш уровень доступа к API – см. ApiModes.
        ///• Количество API-запросов по вашей компании за текущий месяц.
        ///• Количество доступных каналов.
        ///• Установленный web_hook
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Info ApiInfo()
        {
            var url = $"{BaseUrl}/api_info";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Info>(response, "data");
        }

        /// <summary>
        /// Возвращает имеющиеся уровни доступа к API. Ваш текущий уровень  Ваш текущий уровень доступа – см. ApiInfo.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public List<ApiMode> ApiModes()
        {
            var url = $"{BaseUrl}/help/api_modes";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<List<ApiMode>>(response, "data");
        }

        #region Messages

        /// <summary>
        /// Возвращает расширенную информацию о сообщении
        /// </summary>
        /// <param name="id">id сообщения</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Id сообщения не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Message GetMessage(int id)
        {
            if (id <= 0) throw new ArgumentException("Id сообщения не может быть меньше или равен 0", "id");
            var url = $"{_messagesBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Message>(response, "data");
        }

        /// <summary>
        /// Возвращает накопившийся список сообщений (как от клиентов, так и к клиентам).
        /// При запросе поддерживается фильтрация списка по полям. • transport • channel_id • client_id • type (to_client или from_client) • dialog_id • read (прочитано или нет сообщение оператором) 
        /// </summary>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0)</param>
        /// <param name="limit">количество возвращаемых записей (по умолчанию – 50). </param>
        /// <param name="channelId">Id канала</param>
        /// <param name="clientId">Id клиента</param>
        /// <param name="dialogId">Id диалога.</param>
        /// <param name="read">прочитано или нет сообщение оператором</param>
        /// <param name="transport">транспорт по которому пришло сообщение</param>
        /// <param name="type">тип сообщения</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Message> GetMessages(int offset = 0, int limit = 50, int? channelId = null, int? clientId = null, int? dialogId = null, Read read = default(Read), Transport transport = default(Transport), MessageFrom type = default(MessageFrom))
        {
            var url = string.Empty;
            url = $"{_messagesBaseUrl}";
            var param = new Dictionary<string, string>();
            if (channelId != null)
                param.Add("channel_id", channelId.ToString());
            if (clientId != null)
                param.Add("client_id", clientId.ToString());
            if (dialogId != null)
                param.Add("dialog_id", dialogId.ToString());
            if (read != default(Read))
                param.Add("read", read.GetStringValue());
            if (transport != default(Transport))
                param.Add("transport", transport.GetStringValue());
            if (type != default(MessageFrom))
                param.Add("type", type.GetStringValue());
            if (param.Count > 0)
            {
                url = param.Aggregate(url, (current, item) => $"{current}&{item.Key}={item.Value}");
                url = url.ReplaceCharInString(37, '?');
                url = $"{url}&offset={offset}&limit={limit}";
            }
            else
                url = $"{url}?offset={offset}&limit={limit}";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Message>>(response);
        }

        /// <summary>
        /// Помечает сообщение как прочитанное или непрочитанное оператором. Имейте в виду, что при ответе на сообщение, это сообщение и весь диалог автоматически помечаются как прочитанные. 
        /// </summary>
        /// <param name="id">id сообщения</param>
        /// <param name="read">статус сообщения</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Id сообщения не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse SetMessageRead(int id, Read read)
        {
            if (id <= 0) throw new ArgumentException("Id сообщения не может быть меньше или равен 0", "id");
            var url = read == Read.Read ? $"{_messagesBaseUrl}/{id}/read" : $"{_messagesBaseUrl}/{id}/unread";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        #endregion

        #region Channels 

        /// <summary>
        /// Возвращает список ваших каналов с их названиями, поддерживаемыми транспортами и номерами телефона. Поддерживается фильтрация списка по полю телефон
        /// </summary>
        /// <param name="phone">телефон</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Channel> GetChannels(string phone = null)
        {
            var offset = 0;
            var limit = 50;
            var url = string.IsNullOrWhiteSpace(phone) ? $"{_channelsBaseUrl}?offset={offset}&limit={limit}" : $"{_channelsBaseUrl}?phone={phone}&offset={offset}&limit={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Channel>>(response);
        }

        #endregion

        #region Clients      

        /// <summary>
        /// Возвращает клиента с информацией по нему
        /// </summary>
        /// <param name="id">id клиента</param>
        /// <exception cref="ArgumentException">Id клиента не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Client GetClient(int id)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            var url = $"{_clientsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Client>(response, "data");
        }

        /// <summary>
        /// Возвращает список ваших клиентов
        /// </summary>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Client> GetClients(int offset = 0, int limit = 20)
        {
            var url = $"{_clientsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Client>>(response);
        }

        /// <summary>
        /// Возвращает список ваших клиентов c фильтрацией по телефону
        /// </summary>
        /// <param name="phone">телефон</param>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="ArgumentException">телефон не может быть пустым - phone</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Client> GetClientsByPhone(string phone, int offset = 0, int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("телефон не может быть пустым", "phone");
            var url = $"{_clientsBaseUrl}?phone={phone}&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Client>>(response);
        }

        /* /// <summary>
         /// Возвращает список диалогов с клиентом (ID не явс-ся id диалога)
         /// </summary>
         /// <param name="id">id клиента</param>
         /// <exception cref="System.ArgumentException">Id клиента не может быть меньше или равен 0 - id</exception>
         /// <exception cref="TokenException">Ошибка токена</exception>
         public void GetClientDialogs(int id)
         {
             if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
             // var url = $"{ClientsBaseUrl}/{id}/dialogs";
             //  var response = _httpService.Request(url, HttpMethod.GET);
         }
         */

        /// <summary>
        /// Возвращает список транспортов клиента
        /// </summary>
        /// <param name="id">id клиента</param>
        /// <exception cref="ArgumentException">Id клиента не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public List<ClientTransports> GetClientTransports(int id)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            var url = $"{_clientsBaseUrl}/{id}/transport";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<List<ClientTransports>>(response, "data");
        }

        /// <summary>
        /// Возвращает список ваших клиентов c фильтрацией по id тегов
        /// </summary>
        /// <param name="tagsId">id тегов</param>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Client> GetClientsByTags(IEnumerable<int> tagsId, int offset = 0, int limit = 20)
        {
            var url = $"{_clientsBaseUrl}?tags=";
            foreach (var tagId in tagsId)
            {
                url += $"{tagId},";
            }
            url = url.Remove(url.Length - 1, 1);
            url = $"{url}&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Client>>(response);
        }

        /// <summary>
        /// Обновляет клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="name">Имя</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Id клиента не может быть меньше или равен 0 - id
        /// или
        /// Имя не может быть пустым - name
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse UpdateClient(int id, string name)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым", "name");
            var url = $"{_clientsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.PUT, new { nickname = name });
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        #endregion

        #region Operators        
        /// <summary>
        /// Возвращает список ваших операторов в системе. 
        /// </summary>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Operator> GetOperators()
        {
            var url = $"{BaseUrl}/operators";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Operator>>(response);
        }

        #endregion

        #region Dialogs  

        /// <summary>
        /// Возвращает диалог
        /// </summary>
        /// <param name="id">id диалога</param>
        /// <exception cref="ArgumentException">Id диалога не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Dialog GetDialog(int id)
        {
            if (id <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "id");
            var url = $"{_dialogsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Dialog>(response, "data");
        }

        /// <summary>
        /// Возвращает список ваших диалогов
        /// </summary>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Dialog> GetDialogs(int offset = 0, int limit = 20)
        {
            var url = $"{_dialogsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Dialog>>(response);
        }

        /// <summary>
        /// Возвращает список диалогов с фильтрацией по оператору
        /// </summary>
        /// <param name="operatorId">Id оператора</param>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="ArgumentException">Id оператора не может быть меньше или равен 0 - operatorId</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Dialog> GetDialogsByOperator(int operatorId, int offset = 0, int limit = 20)
        {
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");
            var url = $"{_dialogsBaseUrl}?operator_id={operatorId}&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Dialog>>(response);
        }

        /// <summary>
        /// Возвращает список диалогов с фильтрацией по статусу диалога
        /// </summary>
        /// <param name="state">статус диалога</param>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Dialog> GetDialogsByState(DialogState state, int offset = 0, int limit = 20)
        {
            var url = state == DialogState.Closed ? $"{_dialogsBaseUrl}?state=closed&offset={offset}&limits={limit}" : $"{_dialogsBaseUrl}?state=open&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Dialog>>(response);
        }

        /// <summary>
        /// Обновляет оператора диалога
        /// </summary>
        /// <param name="dialogId">id диалога</param>
        /// <param name="operatorId">id оператора</param>
        /// <exception cref="System.ArgumentException">
        /// Id диалога не может быть меньше или равен 0 - dialogId
        /// или
        /// Id оператора не может быть меньше или равен 0 - operatorId
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse UpdateDialog(int dialogId, int operatorId)
        {
            if (dialogId <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "dialogId");
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");

            var url = $"{BaseUrl}/dialogs/{dialogId}";
            var param = new { operator_id = operatorId };
            _httpService.Request(url, Method.PUT, param);
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        /// <summary>
        /// Обновляет статус диалога
        /// </summary>
        /// <param name="dialogId">id диалога</param>
        /// <param name="state">статус диалога</param>
        /// <exception cref="ArgumentException">Id диалога не может быть меньше или равен 0 - dialogId</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse UpdateDialog(int dialogId, DialogState state)
        {
            if (dialogId <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "dialogId");
            var url = $"{BaseUrl}/dialogs/{dialogId}";
            var param = new { state = state == DialogState.Open ? "open" : "closed" };
            _httpService.Request(url, Method.PUT, param);
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        /// <summary>
        /// Обновляет статус и оператора диалога
        /// </summary>
        /// <param name="dialogId">id диалога</param>
        /// <param name="operatorId">id оператора</param>
        /// <param name="state">статус диалога</param>
        /// <exception cref="ArgumentException">
        /// Id диалога не может быть меньше или равен 0 - dialogId
        /// или
        /// Id оператора не может быть меньше или равен 0 - operatorId
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse UpdateDialog(int dialogId, int operatorId, DialogState state)
        {
            if (dialogId <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "dialogId");
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");
            var url = $"{BaseUrl}/dialogs/{dialogId}";
            var param = new { operator_id = operatorId, state = state == DialogState.Open ? "open" : "closed" };
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        #endregion

        #region Templates

        /// <summary>
        /// Возвращает шаблон
        /// </summary>
        /// <param name="id">id шаблона</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Template GetTemplate(int id)
        {
            if (id <= 0) throw new ArgumentException("Id шаблона не может быть меньше или равен 0", "id");
            var url = $"{_templatesBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Template>(response, "data");
        }

        /// <summary>
        /// Возвращает список ваших шаблонов для быстрых ответов.
        /// </summary>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Template> GetTemplates(int offset = 0, int limit = 20)
        {
            var url = $"{_templatesBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Template>>(response);
        }

        #endregion

        #region Tags

        /// <summary>
        /// Возвращает тег
        /// </summary>
        /// <param name="id">id тега</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Tag GetTag(int id)
        {
            if (id <= 0) throw new ArgumentException("Id тега не может быть меньше или равен 0", "id");
            var url = $"{_tagsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.ParseSelectToken<Tag>(response, "data");
        }
        /// <summary>
        /// Возвращает ваш список тегов.
        /// </summary>
        /// <param name="offset">смещение относительно 1-й записи списка (по умолчанию – 0).</param>
        /// <param name="limit"> количество возвращаемых записей (по умолчанию – 20).</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public Response<Tag> GetTags(int offset = 0, int limit = 20)
        {
            var url = $"{_tagsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<Response<Tag>>(response);
        }

        /// <summary>
        /// Присваивает теги клиенту. 
        /// </summary>
        /// <param name="clientId">id клиента</param>
        /// <param name="tags">id тегов</param>
        /// <exception cref="ArgumentException">Id клиента не может быть меньше или равен 0 - clientId</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse AssignTags(int clientId, params int[] tags)
        {
            if (clientId <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "clientId");
            var url = $"{_tagsBaseUrl}/assign_to";
            var param = new { tag_ids = tags, assignee_type = "client", assignee_id = clientId };
            var response = _httpService.Request(url, Method.POST, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        /// <summary>
        /// Удаляет тег клиента. 
        /// </summary>
        /// <param name="tagId">id тега</param>
        /// <param name="clientId">id клиента</param>
        /// <exception cref="ArgumentException">
        /// Id тега не может быть меньше или равен 0 - tagId
        /// или
        /// Id клиента не может быть меньше или равен 0 - clientId
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse DeleteTags(int tagId, int clientId)
        {
            if (tagId <= 0) throw new ArgumentException("Id тега не может быть меньше или равен 0", "tagId");
            if (clientId <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "clientId");

            var url = $"{_tagsBaseUrl}/{tagId}/delete_from";
            var param = new { client_id = clientId };
            var response = _httpService.Request(url, Method.DELETE, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse>(response);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat2DeskClient"/> class.
        /// </summary>
        /// <param name="chat2DeskToken">chat2desk token.</param>
        /// <param name="webProxy">The web proxy.</param>
        /// <param name="disableSSLValidate">Disable SSL validate.</param>
        /// <exception cref="ArgumentException">Токен не может быть пустым - chat2DeskToken</exception>
        public Chat2DeskClient(string chat2DeskToken, IWebProxy webProxy = null, bool disableSSLValidate = true)
        {
            if (string.IsNullOrWhiteSpace(chat2DeskToken)) throw new ArgumentException("Токен не может быть пустым", "chat2DeskToken");
            _httpService = webProxy != null ? new HttpService(chat2DeskToken, webProxy) : new HttpService(chat2DeskToken);
            _responseParser = new ResponseParser();

            // отключение проверки сертификатов
            if (disableSSLValidate)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
          
        }

    }
}