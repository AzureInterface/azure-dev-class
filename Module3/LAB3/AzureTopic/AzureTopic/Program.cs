using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace AzureTopic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the connection
            var conn = "Endpoint=sb://mikepfsb.servicebus.windows.net/;SharedAccessKeyName=mypolicy2;SharedAccessKey=[update me]";
            var topic = "mikepftopic";
            var nsm = NamespaceManager.CreateFromConnectionString(conn);

            //Create a subscription if it doesn't already exist

            if (!nsm.SubscriptionExists(topic, "MyMessages"))
            {
                nsm.CreateSubscription(topic, "MyMessages");
            }

            //Send a message to the topic
            var topicClient = TopicClient.CreateFromConnectionString(conn, topic);
            var message = new BrokeredMessage("Hello world!");
            topicClient.Send(message);

            //Read the message
            //var subClient = SubscriptionClient.CreateFromConnectionString(conn, topic, "MyMessages");
            //subClient.OnMessage(m => {
            //    Console.WriteLine("Message: " + m.GetBody<string>());
            //});

            Console.ReadKey();
        }
    }
}
