using System;
using System.Collections.Generic;
using Azure.ServiceBus.Configuration;
using Azure.ServiceBus.Handlers;
using Azure.ServiceBus.Models;
using Microsoft.Azure.ServiceBus;
using StructureMap;

namespace Azure.ServiceBus.Bus
{
    public class RecieveOnlyBus : IRecieveOnlyBus
    {
        private Container container;
        private readonly MessageRouteTable routes;
        private readonly IBusConfigutration configuration;

        private readonly Dictionary<string, IClientEntity> queueClients =
            new Dictionary<string, IClientEntity>();

        private readonly Dictionary<string, List<IClientEntity>> topicClients =
            new Dictionary<string, List<IClientEntity>>();

        private RecieveOnlyBus(IBusConfigutration configuration, MessageRouteTable routes = null)
        {
            this.configuration = configuration;
            this.routes = routes ?? new MessageRouteTable();
        }

        public static IRecieveOnlyBus Initialise(IBusConfigutration configuration, MessageRouteTable routes = null)
        {
            routes = routes ?? new MessageRouteTable();

            var bus = new RecieveOnlyBus(configuration, routes)
            {
                container = new Container(x =>
                {
                    x.For<ISendOnlyBus>().Use(new SendOnlyBus(configuration, routes)).Singleton();
                })
            };

            return bus;
        }

        public IRecieveOnlyBus RegisterQueue<T>(string route, HandlerConfiguration config = null)
        {
            routes.AddRoute<T>(route);

            var handler = container.GetInstance<IHandler<T>>();
            var client = new QueueClient(configuration.ConnectionString, route);

            var handlerConfiguration = config ?? new HandlerConfiguration();

            client.RegisterMessageHandler((message, token) => handler.Handle(message, client, token),
                handlerConfiguration.Options(handler.OnException));

            if (queueClients.ContainsKey(route))
            {
                throw new Exception("Queues can only have a single handler registered.");
            }

            queueClients.Add(route, client);
            return this;
        }

        public IRecieveOnlyBus RegisterTopic<T>(string route, HandlerConfiguration config = null)
        {
            routes.AddRoute<T>(route);

            var handlers = container.GetAllInstances<ITopicMessageHandler<T>>();

            var handlerConfiguration = config ?? new HandlerConfiguration();

            foreach (var handler in handlers)
            {
                var client = new SubscriptionClient(configuration.ConnectionString, route, handler.Subscription);

                client.RegisterMessageHandler((message, token) => handler.Handle(message, client, token),
                    handlerConfiguration.Options(handler.OnException));

                if (topicClients.ContainsKey(route))
                {
                    topicClients[route].Add(client);
                }
                else
                {
                    topicClients.Add(route, new List<IClientEntity> {client});
                }
            }

            return this;
        }

        public IRecieveOnlyBus WithDependencyRegistrations(Action<DependencyRegistration> configure)
        {
            container.Configure(x => configure(new DependencyRegistration(x)));

            return this;
        }

        public void Dispose()
        {
            foreach (var client in queueClients)
            {
                client.Value.CloseAsync().Wait();
            }

            foreach (var clients in topicClients)
            {
                foreach (var client in clients.Value)
                {
                    client.CloseAsync().Wait();
                }
            }
        }
    }
}