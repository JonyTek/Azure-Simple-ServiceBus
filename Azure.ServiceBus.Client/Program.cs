using System;
using System.Threading.Tasks;
using Azure.ServiceBus.Bus;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var routes = new MessageRouteTable()
                .AddRoute<MyQueueMessage>("my-message-queue")
                .AddRoute<MyTopicMessage>("my-message-topic");

            var bus = new SendOnlyBus(new BusConfigutration(), routes);

            await bus.Send(new MyQueueMessage { SomeString = "From my queue!" });
            await bus.Publish(new MyTopicMessage { SomeString = "From my topic!" });

            Console.ReadKey();
        }
    }
}
