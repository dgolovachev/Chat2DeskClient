# Chat2DeskClient
C# клиент для работы с сервисом Chat2Desk

Пример получения сообщений:
```cs
var client = new Chat2DeskClient("token");
var response = client.GetMessages(100);

foreach (var message in response.Data)
{
    Console.WriteLine(message.Created + " " + message.Text);
}

Console.ReadKey();
```
