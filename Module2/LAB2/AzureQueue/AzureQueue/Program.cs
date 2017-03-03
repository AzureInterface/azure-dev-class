using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace AzureQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add an item into a queue named mikepfq
            var conn = "Endpoint=sb://mikepfsb.servicebus.windows.net/;SharedAccessKeyName=mypolicy;SharedAccessKey=[update me];EntityPath=mikepfq";

            var client = QueueClient.CreateFromConnectionString(conn);

            var message = new BrokeredMessage("Hello world!");
            client.Send(message);

            //Read the message
            var options = new OnMessageOptions
            {
                AutoComplete = false
            };

            client.OnMessage(m =>
            {
                Console.WriteLine("Message: " + m.GetBody<string>());
                m.Complete();
            }, options);

            Console.ReadKey();
        }
    }
}
