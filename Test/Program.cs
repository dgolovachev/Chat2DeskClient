using System;
using Chat2Desk;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Chat2DeskClient("TOKEN");
            var firstMessageId = client.GetFirstMessageId();
            var messages = client.GetMessages(firstMessageId);

            foreach (var message in messages.Data)
            {
                Console.WriteLine($"ID: {message.Id}, Text:{message.Text}, Transport: {message.Transport}");
            }

            Console.ReadKey();
        }
    }
}