﻿using System;
using System.Threading.Tasks;
using Azure.ServiceBus.Bus;
using Azure.ServiceBus.Handlers.Handlers;
using Azure.ServiceBus.Messages;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.Handlers
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultHandlerConfig = new HandlerConfiguration {MaxConcurrentCalls = 10};

            var bus = RecieveOnlyBus.Initialise(new BusConfigutration())
                .WithDependencyRegistrations(x =>
                {
                    x.RegisterQueueHandler<MyQueueMessage, MyMessageQueueMessageHandler>();
                    x.RegisterTopicHandler<MyTopicMessage, MyMessageTopicMessageHandler>();
                    x.RegisterTopicHandler<MyTopicMessage, AnotherMyMessageTopicMessageHandler>();
                })
                .RegisterQueue<MyQueueMessage>("my-message-queue", defaultHandlerConfig)
                .RegisterTopic<MyTopicMessage>("my-message-topic", defaultHandlerConfig);

            Console.WriteLine("Waiting.......");

            Console.ReadKey();

            bus.Dispose();
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}