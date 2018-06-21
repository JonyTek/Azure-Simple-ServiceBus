using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Handlers.Handlers
{
    public class MyMessageQueueMessageHandler : AbstractQueueMessageHandler<MyQueueMessage>
    {
        public override Task Handle(MessageContext<MyQueueMessage> messageContext, CancellationToken token)
        {
            Console.WriteLine("MyMessageQueueMessageHandle " + messageContext.Message.SomeString);

            return Task.FromResult(0);
        }
    }
}