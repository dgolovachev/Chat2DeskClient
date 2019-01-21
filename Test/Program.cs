using System;
using System.IO;
using System.Linq;
using Chat2Desk;
using Chat2Desk.Types.Enums;
using Chat2Desk.Utils;

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
                Console.WriteLine($"ID: {message.Id}, Text:{message.Text}, Transport: {message.Transport.GetStringValue()}");
            }

            Console.ReadKey();
        }
    }
}