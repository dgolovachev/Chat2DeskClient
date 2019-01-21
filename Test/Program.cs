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
            // var client = new Chat2DeskClient("97d35f4783c70c8b7412a3068cc43b"); // imarat
            var client = new Chat2DeskClient("d1bdb8e80fc2c4d3050d49a10a433d");// crm


            var response = client.CreateClient("996554644662", Transport.Whatsapp, 0, "Дмитрий Головачев");

            if (response.Status == ResponseStatus.Success)
            {
                Console.WriteLine(response.Data.Id + " " + response.Data.AssignedName);
                var messageReponse = client.SendMessage(response.Data.Id, "TEST123", Transport.Whatsapp);
                if (messageReponse.Status == ResponseStatus.Success) Console.WriteLine(messageReponse.Data.MessageId);
                else Console.WriteLine("all bad");
            }
            else Console.WriteLine("bad");



            //  var client = new Chat2DeskClient("97d35f4783c70c8b7412a3068cc43b");
            //var messages = client.GetMessages(2550);

            //foreach (var message in messages.Data)
            //{
            //    Console.WriteLine($"ID: {message.Id}, Text:{message.Text}, Transport: {message.Transport.GetStringValue()}");
            //}

            Console.ReadKey();
        }
    }
}