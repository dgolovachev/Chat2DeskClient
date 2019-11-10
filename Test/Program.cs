using System;
using Chat2Desk;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Chat2DeskClient("token");
            var messages = client.GetMessages(2550);

            foreach (var message in messages.Data)
            {
                Console.WriteLine($"ID: {message.Id}, Text:{message.Text}, Transport: {message.Transport}");
            }

            Console.ReadKey();
        }
    }
}