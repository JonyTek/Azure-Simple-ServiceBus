using System.Text;
using System.Threading.Tasks;
using Azure.ServiceBus.Configuration;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Azure.ServiceBus.Bus
{
    public class SendOnlyBus : ISendOnlyBus
    {
        private readonly IBusConfigutration configuration;
        private readonly MessageRouteTable routes;

        public SendOnlyBus(IBusConfigutration configuration, MessageRouteTable routes = null)
        {
            this.configuration = configuration;
            this.routes = routes ?? new MessageRouteTable();
        }

        public async Task Send<T>(T instance)
            where T : IQueueMessage
        {
            var client = new QueueClient(configuration.ConnectionString, GetRoute(instance));
            await Broadcast(client, instance);
        }

        public async Task Publish<T>(T instance)
            where T : ITopicMessage
        {
            var client = new TopicClient(configuration.ConnectionString, GetRoute(instance));
            await Broadcast(client, instance);
        }

        private static async Task Broadcast<T>(ISenderClient client, T instance)
        {
            var body = JsonConvert.SerializeObject(instance);
            var message = new Message(Encoding.UTF8.GetBytes(body));

            await client.SendAsync(message);
            await client.CloseAsync();
        }

        private string GetRoute<T>(T instance)
        {
            return routes.GetRoute(instance);
        }
    }
}