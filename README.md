# Chat2DeskClient
## C# клиент для работы с сервисом Chat2Desk

### Пример получения сообщений:
```cs
var client = new Chat2DeskClient("token");

var response = client.GetMessages(100);

foreach (var message in response.Data)
{
    Console.WriteLine(message.Created + " " + message.Text);
}

Console.ReadKey();
```

### Пример отправки сообщения:
```cs
var client = new Chat2DeskClient("token");
var reponse = client.SendMessage(response.Data.Id, "Hi", Transport.Whatsapp);
if (reponse.Status == ResponseStatus.Success) 
    Console.WriteLine(reponse.Data.MessageId);

Console.ReadKey();
```
### Пример добавления клиента:
```cs
var client = new Chat2DeskClient("token");
var response = client.CreateClient("996554644662", Transport.Whatsapp, 0, "Дмитрий Головачев");
if (response.Status == ResponseStatus.Success)
    Console.WriteLine(response.Data.Id + " " + response.Data.AssignedName);
    
Console.ReadKey();
```
