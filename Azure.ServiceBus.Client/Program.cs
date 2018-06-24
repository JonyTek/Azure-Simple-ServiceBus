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

            while (true)
            {
                Console.WriteLine("Please enter option:");
                Console.WriteLine("Q: queue");
                Console.WriteLine("T: topic");
                Console.WriteLine("X: exit");
                Console.WriteLine();

                var option = Console.ReadKey();
                var bus = new SendOnlyBus(new BusConfigutration(), routes);

                switch (option.Key)
                {
                    case ConsoleKey.X:
                        return;
                    case ConsoleKey.Q:
                        Console.WriteLine();
                        Console.WriteLine($"Please enter body as text.");
                        await bus.Send(new MyQueueMessage {SomeString = Console.ReadLine()});
                        continue;
                    case ConsoleKey.T:
                        Console.WriteLine();
                        Console.WriteLine($"Please enter body as text.");
                        await bus.Publish(new MyTopicMessage {SomeString = Console.ReadLine()});
                        continue;
                    default:
                        continue;
                }



                //await bus.Send(new MyQueueMessage { SomeString = "From my queue!" });
                //await bus.Publish(new MyTopicMessage { SomeString = "From my topic!" });
            }
        }
    }
}
