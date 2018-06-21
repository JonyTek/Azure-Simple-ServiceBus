using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Bus;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Handlers.Handlers
{
    public class MyMessageTopicMessageHandler : AbstractTopicMessageHandler<MyTopicMessage>
    {
        private readonly ISendOnlyBus bus;
        public override string Subscription => "my-subscription";

        public MyMessageTopicMessageHandler(ISendOnlyBus bus)
        {
            this.bus = bus;
        }
        
        public override async Task Handle(MessageContext<MyTopicMessage> messageContext, CancellationToken token)
        {
            Console.WriteLine("MyMessageTopicMessageHandle " + messageContext.Message.SomeString);

            try
            {

                await bus.Send(new MyQueueMessage { SomeString = " Resent!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(">>>>>" + e.Message);
            }
        }
    }
}