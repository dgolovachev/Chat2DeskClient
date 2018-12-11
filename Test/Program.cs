using System;
using Chat2Desk;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Chat2DeskClient("token");
            var response = client.GetMessages(100);

            foreach (var message in response.Data)
            {
                Console.WriteLine(message.Created + " " + message.Text);
            }

            Console.ReadKey();
        }
    }
}