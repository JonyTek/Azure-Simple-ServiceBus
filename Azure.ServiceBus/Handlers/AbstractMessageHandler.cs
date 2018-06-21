using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Azure.ServiceBus.Handlers
{
    public abstract class AbstractMessageHandler<T>
    {
        public abstract Task Handle(MessageContext<T> messageContext, CancellationToken token);

        public async Task Handle<TClient>(Message message, TClient client, CancellationToken token)
            where TClient : IReceiverClient
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            var instance = JsonConvert.DeserializeObject<T>(messageBody);
            var context = new MessageContext<T> {Message = instance, Context = message};

            await Handle(context, token);
            await client.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}