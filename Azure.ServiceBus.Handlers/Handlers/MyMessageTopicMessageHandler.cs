using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Bus;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;
using Microsoft.Azure.ServiceBus;

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
            
            await bus.Send(new MyQueueMessage {SomeString = " Resent!!"});
        }

        public override Task OnException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Exception: {exceptionReceivedEventArgs.Exception.Message}");
            Console.WriteLine($"Action: {exceptionReceivedEventArgs.ExceptionReceivedContext.Action}");
            Console.WriteLine($"ClientId: {exceptionReceivedEventArgs.ExceptionReceivedContext.ClientId}");
            Console.WriteLine($"Endpoint: {exceptionReceivedEventArgs.ExceptionReceivedContext.Endpoint}");
            Console.WriteLine($"EntityPath: {exceptionReceivedEventArgs.ExceptionReceivedContext.EntityPath}");

            return base.OnException(exceptionReceivedEventArgs);
        }
    }
}