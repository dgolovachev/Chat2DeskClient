using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private const string TokenErrorMessage = "You have to specify your token. See API manual on info@chat2desk.com";
        private const string ApiRequestExceededMessage = "Number of API requests exceeded";
        private const string ApiCallsExeededMessage = "API calls exceeded API limit per month";
        private const string PageNotFoundMessage = "Page not found";

        /// <summary>
        /// Проверяет на ошибки запросов к API
        /// </summary>
        /// <param name="response"></param>  
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        private void CheckResponse(string response)
        {
            if (response.Contains(AuthErrorMessage)) throw new TokenException("You need to auth");
            if (response.Contains(TokenErrorMessage)) throw new TokenException("You have to specify your token. See API manual on info@chat2desk.com");
            if (response.Contains(ApiRequestExceededMessage)) throw new APIExceededException("Number of API requests exceeded");
            if (response.Contains(ApiCallsExeededMessage)) throw new APIExceededException("API calls exceeded API limit per month");
            if (response.Contains(PageNotFoundMessage)) throw new PageNotFounException("Page not found");
        }

        /// <summary>
        /// Установка web hook, допускается любой URL, но рекомендуется https. Если передаётся пустое значение (null), то web hook удаляется.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<object> WebHook(string url = null)
        {
            var webHookUrl = $"{BaseUrl}/companies/web_hook";
            var response = _httpService.Request(webHookUrl, Method.POST, string.IsNullOrWhiteSpace(url) ? new { url = string.Empty } : new { url = url });
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
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
        public ApiResponse<Info> ApiInfo()
        {
            var url = $"{BaseUrl}/companies/api_info";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Info>>(response);
        }

        /// <summary>
        /// Возвращает имеющиеся уровни доступа к API. Ваш текущий уровень  Ваш текущий уровень доступа – см. ApiInfo.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<List<ApiMode>> ApiModes()
        {
            var url = $"{BaseUrl}/help/api_modes";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<ApiMode>>>(response);
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
        public ApiResponse<Message> GetMessage(int id)
        {
            if (id <= 0) throw new ArgumentException("Id сообщения не может быть меньше или равен 0", "id");
            var url = $"{_messagesBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Message>>(response);
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
        /// <param name="startDate">Выборка сообщений от выбранной даты</param>
        /// <param name="finishDate">Выборка сообщений до выбранной даты</param>
        /// <param name="operatorId">Id оператора</param>
        /// <param name="order">Сортировка сообщений по порядку либо с конца либо с начала</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        /// 
        [ObsoleteAttribute("This method is obsolete. Call GetMessages instead.", true)]
        public ApiResponse<List<Message>> GetMessagesByOffset(int offset = 0, int limit = 50, int? channelId = null, int? clientId = null, int? dialogId = null,
                                            Read read = default(Read), Transport transport = default(Transport), MessageFrom type = default(MessageFrom),
                                            string startDate = null, string finishDate = null, int? operatorId = null, Order order = default(Order))
        {
            var url = string.Empty;
            url = $"{_messagesBaseUrl}";
            var param = new Dictionary<string, string>();
            if (channelId != null) param.Add("channel_id", channelId.ToString());
            if (clientId != null) param.Add("client_id", clientId.ToString());
            if (dialogId != null) param.Add("dialog_id", dialogId.ToString());
            if (read != default(Read)) param.Add("read", read.GetStringValue());
            if (transport != default(Transport)) param.Add("transport", transport.GetStringValue());
            if (type != default(MessageFrom)) param.Add("type", type.GetStringValue());
            if (startDate != null) param.Add("start_date", startDate);
            if (finishDate != null) param.Add("finish_date", finishDate);
            if (operatorId != null) param.Add("operator_id", operatorId.ToString());
            if (order != default) param.Add("order", order.GetStringValue());
            if (param.Count > 0)
            {
                url = param.Aggregate(url, (current, item) => $"{current}&{item.Key}={item.Value}");
                url = url.ReplaceCharInString(37, '?');
                url = $"{url}&offset={offset}&limit={limit}";
            }
            else url = $"{url}?offset={offset}&limit={limit}";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Message>>>(response);
        }

        /// <summary>
        /// Возвращает накопившийся список сообщений (как от клиентов, так и к клиентам).
        /// При запросе поддерживается фильтрация списка по полям. • transport • channel_id • client_id • type (to_client или from_client) • dialog_id • read (прочитано или нет сообщение оператором) 
        /// </summary>
        /// <param name="startId">смещение по id сообщения</param>
        /// <param name="limit">количество возвращаемых записей (по умолчанию – 50). </param>
        /// <param name="channelId">Id канала</param>
        /// <param name="clientId">Id клиента</param>
        /// <param name="dialogId">Id диалога.</param>
        /// <param name="read">прочитано или нет сообщение оператором</param>
        /// <param name="transport">транспорт по которому пришло сообщение</param>
        /// <param name="type">тип сообщения</param> 
        /// <param name="startDate">Выборка сообщений от выбранной даты</param>
        /// <param name="finishDate">Выборка сообщений до выбранной даты</param>
        /// <param name="operatorId">Id оператора</param>
        /// <param name="order">Сортировка сообщений по порядку либо с конца либо с начала</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        /// 
        public ApiResponse<List<Message>> GetMessages(int startId = 0, int limit = 50, int? channelId = null, int? clientId = null, int? dialogId = null,
                                           Read read = default(Read), Transport transport = default(Transport), MessageFrom type = default(MessageFrom),
                                             string startDate = null, string finishDate = null, int? operatorId = null, Order order = default(Order))
        {
            var url = string.Empty;
            url = $"{_messagesBaseUrl}";
            var param = new Dictionary<string, string>();
            if (channelId != null) param.Add("channel_id", channelId.ToString());
            if (clientId != null) param.Add("client_id", clientId.ToString());
            if (dialogId != null) param.Add("dialog_id", dialogId.ToString());
            if (read != default(Read)) param.Add("read", read.GetStringValue());
            if (transport != default(Transport)) param.Add("transport", transport.GetStringValue());
            if (type != default(MessageFrom)) param.Add("type", type.GetStringValue());
            if (startId != 0) param.Add("start_id", startId.ToString());
            if (startDate != null) param.Add("start_date", startDate);
            if (finishDate != null) param.Add("finish_date", finishDate);
            if (operatorId != null) param.Add("operator_id", operatorId.ToString());
            if (order != default(Order)) param.Add("order", order.GetStringValue());

            if (param.Count > 0)
            {
                url = param.Aggregate(url, (current, item) => $"{current}&{item.Key}={item.Value}");
                url = url.ReplaceCharInString(37, '?');
                url = $"{url}&limit={limit}";
            }
            else url = $"{url}?limit={limit}";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Message>>>(response);
        }

        /// <summary>
        /// Выдает самое первое сообщение
        /// </summary>
        public int GetFirstMessageId(Transport transport = default(Transport))
        {
            string url = String.Empty;
            var limit = 1;
            if (transport != default(Transport)) url = $"{_messagesBaseUrl}?transport={transport.GetStringValue()}&limit={limit}&order=asc";
            else url = $"{_messagesBaseUrl}?&limit={limit}&order=asc";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            var messages = _responseParser.Parse<ApiResponse<List<Message>>>(response);
            var messsage = messages.Data.FirstOrDefault();
            if (messsage == null) return 0;
            return messsage.Id;
        }

        /// <summary>
        /// Отправляет сообщение клиенту
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="text"></param>
        /// <param name="transport">транспорт по которому пришло сообщение</param>
        /// <param name="channelId"></param>
        /// <param name="operatorId"></param>
        /// <param name="attachment">url адрес вдлжения фотографии. Только прямые url адреса поддерживаются</param>
        /// <param name="pdf">url адресс для влодения фотографии. Поддерживаются только прямые url</param>
        /// <param name="openDialog">открыть или не закрыть диалог при отправке сообщение в него</param>
        /// <param name="messageType">Тип сообщения</param>
        /// <param name="encrypted">зашифровывает сообщение при отправке но пользователь получает расшифроавнное</param>
        /// <param name="externalId">если вы отправляете сообщение через интеграцию с другого платформу обмена сообщениями или CRM-систему, вы можете хранить id сообщение во внешней системе в этом поле, если это необходимо для интеграция.</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<MessageResponse> SendMessage(int clientId, string text, Transport transport, int channelId = default(int), int operatorId = default(int),
                                          string attachment = "", string pdf = "", bool openDialog = false, MessageType messageType = default(MessageType),
                                          bool encrypted = false, int externalId = default(int))
        {
            if (clientId <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "clientId");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Текст не может быть пустым", "text");

            var url = $"{_messagesBaseUrl}";

            dynamic param = new ExpandoObject();
            param.client_id = clientId;
            param.text = text;
            if (!string.IsNullOrWhiteSpace(attachment)) param.attachment = attachment;
            if (!string.IsNullOrWhiteSpace(pdf)) param.pdf = pdf;
            param.type = messageType.GetStringValue();
            //param.type = "to_client";
            param.transport = transport.GetStringValue();
            if (channelId != default(int)) param.channel_id = channelId;
            if (operatorId != default(int)) param.operator_id = operatorId;
            if (externalId != default(int)) param.external_id = externalId;
            param.open_dialog = openDialog;
            param.encrypted = encrypted;
            Console.WriteLine(param.encrypted);
            var response = _httpService.Request(url, Method.POST, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<MessageResponse>>(response);
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
        public ApiResponse<object> SetMessageRead(int id, Read read)
        {
            if (id <= 0) throw new ArgumentException("Id сообщения не может быть меньше или равен 0", "id");
            var url = read == Read.Read ? $"{_messagesBaseUrl}/{id}/read" : $"{_messagesBaseUrl}/{id}/unread";

            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
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
        public ApiResponse<List<Channel>> GetChannels(string phone = null)
        {
            var offset = 0;
            var limit = 50;
            var url = string.IsNullOrWhiteSpace(phone) ? $"{_channelsBaseUrl}?offset={offset}&limit={limit}" : $"{_channelsBaseUrl}?phone={phone}&offset={offset}&limit={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Channel>>>(response);
        }


        /// <summary>
        /// Возвращает канал по переданному Id
        /// </summary>
        /// <param name="id"> id канала</param>
        /// <returns></returns>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<Channel> GetChannelById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id канала не может быть меньше или равен 0", "id");
            var url = $"{_channelsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Channel>>(response);
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
        public ApiResponse<Client> GetClient(int id)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            var url = $"{_clientsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Client>>(response);
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
        public ApiResponse<List<Client>> GetClients(int offset = 0, int limit = 20)
        {
            var url = $"{_clientsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Client>>>(response);
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
        public ApiResponse<List<Client>> GetClientsByPhone(string phone, int offset = 0, int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("телефон не может быть пустым", "phone");
            var url = $"{_clientsBaseUrl}?phone={phone}&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Client>>>(response);
        }

        /// <summary>
        /// Возвращает список транспортов клиента
        /// </summary>
        /// <param name="id">id клиента</param>
        /// <exception cref="ArgumentException">Id клиента не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<List<ClientTransports>> GetClientTransports(int id)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            var url = $"{_clientsBaseUrl}/{id}/transport";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<ClientTransports>>>(response);
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
        public ApiResponse<List<Client>> GetClientsByTags(IEnumerable<int> tagsId, int offset = 0, int limit = 20)
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
            return _responseParser.Parse<ApiResponse<List<Client>>>(response);
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
        public ApiResponse<object> UpdateClient(int id, string name)
        {
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым", "name");
            var url = $"{_clientsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.PUT, new { nickname = name });
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
        }

        /// <summary>
        /// Добавляет клиента в систему
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="transport"></param>
        /// <param name="channelId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Телефон не может быть пустым
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<ClientResponse> CreateClient(string phone, Transport transport = Transport.Whatsapp, int channelId = default(int), string nickname = "")
        {
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Телефон не может быть пустым", "phone");
            var url = $"{_clientsBaseUrl}";

            dynamic param = new ExpandoObject();
            param.phone = phone;
            param.transport = transport.GetStringValue();
            //if (channelId != default(int)) param.channel_id = channelId;
            if (!string.IsNullOrWhiteSpace(nickname)) param.nickname = nickname;

            var response = _httpService.Request(url, Method.POST, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<ClientResponse>>(response);
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
        public ApiResponse<List<Operator>> GetOperators()
        {
            var url = $"{BaseUrl}/operators";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Operator>>>(response);
        }

        /// <summary>
        /// Изменяет статус оператора 
        /// </summary>
        /// <param name="operator_id"> id оператора</param>
        /// <param name="status_id"> id статуса</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Id диалога не может быть меньше или равен 0 - id</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<Operator> ChangeOperatorStatus(int operator_id, int status_id)
        {
            if (operator_id <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operator_id");
            var url = $"{BaseUrl}/operators/{operator_id}";
            dynamic param = new ExpandoObject();
            param.status_id = status_id;
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Operator>>(response);
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
        public ApiResponse<Dialog> GetDialog(int id)
        {
            if (id <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "id");
            var url = $"{_dialogsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Dialog>>(response);
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
        public ApiResponse<List<Dialog>> GetDialogs(int offset = 0, int limit = 20)
        {
            var url = $"{_dialogsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Dialog>>>(response);
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
        public ApiResponse<List<Dialog>> GetDialogsByOperator(int operatorId, int offset = 0, int limit = 20)
        {
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");
            var url = $"{_dialogsBaseUrl}?operator_id={operatorId}&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Dialog>>>(response);
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
        public ApiResponse<List<Dialog>> GetDialogsByState(DialogState state, int offset = 0, int limit = 20)
        {
            var url = state == DialogState.Closed ? $"{_dialogsBaseUrl}?state=closed&offset={offset}&limits={limit}" : $"{_dialogsBaseUrl}?state=open&offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Dialog>>>(response);
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
        public ApiResponse<object> UpdateDialog(int dialogId, int operatorId)
        {
            if (dialogId <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "dialogId");
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");

            var url = $"{BaseUrl}/dialogs/{dialogId}";
            var param = new { operator_id = operatorId };
            _httpService.Request(url, Method.PUT, param);
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
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
        public ApiResponse<object> UpdateDialog(int dialogId, int operatorId, DialogState state)
        {
            if (dialogId <= 0) throw new ArgumentException("Id диалога не может быть меньше или равен 0", "dialogId");
            if (operatorId <= 0) throw new ArgumentException("Id оператора не может быть меньше или равен 0", "operatorId");
            var url = $"{BaseUrl}/dialogs/{dialogId}";
            var param = new { operator_id = operatorId, state = state == DialogState.Open ? "open" : "closed" };
            var response = _httpService.Request(url, Method.PUT, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
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
        public ApiResponse<Template> GetTemplate(int id)
        {
            if (id <= 0) throw new ArgumentException("Id шаблона не может быть меньше или равен 0", "id");
            var url = $"{_templatesBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Template>>(response);
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
        public ApiResponse<List<Template>> GetTemplates(int offset = 0, int limit = 20)
        {
            var url = $"{_templatesBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Template>>>(response);
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
        public ApiResponse<Tag> GetTag(int id)
        {
            if (id <= 0) throw new ArgumentException("Id тега не может быть меньше или равен 0", "id");
            var url = $"{_tagsBaseUrl}/{id}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<Tag>>(response);
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
        public ApiResponse<List<Tag>> GetTags(int offset = 0, int limit = 20)
        {
            var url = $"{_tagsBaseUrl}?offset={offset}&limits={limit}";
            var response = _httpService.Request(url, Method.GET);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<List<Tag>>>(response);
        }

        /// <summary>
        /// Присваивает теги клиенту. 
        /// </summary>
        /// <param name="tagType">Тип привязки тэга</param>
        /// <param name="id">Id клиента либо запроса в зависимости от параметра tagType</param>
        /// <param name="tags">id тегов</param>
        /// <exception cref="ArgumentException">Id клиента не может быть меньше или равен 0 - clientId</exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<object> AssignTags(TagType tagType, int id, IEnumerable<int> tags)
        {
            if (id <= 0) throw new ArgumentException("Id не может быть меньше или равен 0", "id");
            if (tags.Count() == 0) throw new ArgumentException("tags не может быть пустым", "tags");
            var url = $"{_tagsBaseUrl}/assign_to";
            var param = new { tag_ids = tags, assignee_type = tagType.GetStringValue(), assignee_id = id };
            var response = _httpService.Request(url, Method.POST, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
        }

        /// <summary>
        /// Удаляет тег клиента. 
        /// </summary>
        /// <param name="tagType">Тип по удалению тега</param>
        /// <param name="tagId">id тега</param>
        /// <param name="id">id клиета или запроса в зависимости от параметра tagType</param>
        /// <exception cref="ArgumentException">
        /// Id тега не может быть меньше или равен 0 - tagId
        /// или
        /// Id клиента не может быть меньше или равен 0 - clientId
        /// </exception>
        /// <exception cref="TokenException">Ошибка токена</exception>
        /// <exception cref="HttpException">Ошибка Http</exception>
        /// <exception cref="ParseException">Ошибка парсинга</exception>
        /// <exception cref="APIExceededException">Превышен лимит запросов к API</exception>
        public ApiResponse<object> DeleteTags(TagType tagType, int tagId, int id)
        {
            if (tagId <= 0) throw new ArgumentException("Id тега не может быть меньше или равен 0", "tagId");
            if (id <= 0) throw new ArgumentException("Id клиента не может быть меньше или равен 0", "id");

            var url = $"{_tagsBaseUrl}/{tagId}/delete_from";
            dynamic param = new ExpandoObject();
            if (tagType == TagType.Client) param.client_id = id;
            else if (tagType == TagType.Request) param.request_id = id;
            var response = _httpService.Request(url, Method.DELETE, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
        }

        /// <summary>
        /// Создает тег
        /// </summary>
        /// <param name="tag_group_id">id группы тегов</param>
        /// <param name="tag_label">название тега</param>
        /// <param name="tag_description">описание тега</param>
        /// <param name="tag_bg_color">цвет заднего фона тега</param>
        /// <param name="tag_text_color">цвет текста тега</param>
        /// <param name="order_show">используется для сортировки тегов в пользовательском интерфейсе</param>
        /// <returns></returns>
        public ApiResponse<object> CreateTag(int tag_group_id, string tag_label, string tag_description, string tag_bg_color, string tag_text_color, int? order_show = null)
        {
            if (tag_group_id <= 0) throw new ArgumentException("Id группы тега не может быть меньше или равен 0", "tag_group_id");
            if (string.IsNullOrWhiteSpace(tag_label)) throw new ArgumentException("Название тега не может быть путсым", "tag_label");
            if (string.IsNullOrWhiteSpace(tag_description)) throw new ArgumentException("Описание тега не может быть путсым", "tag_description");
            if (string.IsNullOrWhiteSpace(tag_bg_color)) throw new ArgumentException("Цвет заднего фона тега не может быть путсым", "tag_bg_color");
            if (string.IsNullOrWhiteSpace(tag_text_color)) throw new ArgumentException("Цвет тега не может быть путсым", "tag_text_color");
            dynamic param = new ExpandoObject();
            if (order_show != null) param.order_show = order_show;
            param.tag_group_id = tag_group_id;
            param.tag_label = tag_label;
            param.tag_description = tag_description;
            param.tag_bg_color = tag_bg_color;
            param.tag_text_color = tag_text_color;

            var url = $"{_tagsBaseUrl}";
            var response = _httpService.Request(url, Method.POST, param);
            CheckResponse(response);
            return _responseParser.Parse<ApiResponse<object>>(response);
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