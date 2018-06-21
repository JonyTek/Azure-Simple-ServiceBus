using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Handlers.Handlers
{
    public class AnotherMyMessageTopicMessageHandler : AbstractTopicMessageHandler<MyTopicMessage>
    {
        public override string Subscription => "my-subscription-1";

        public override Task Handle(MessageContext<MyTopicMessage> messageContext, CancellationToken token)
        {
            Console.WriteLine("AnotherMyMessageTopicMessageHandle " + messageContext.Message.SomeString);

            return Task.FromResult(0);
        }
    }
}