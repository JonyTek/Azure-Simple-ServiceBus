using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Messages;
using Azure.ServiceBus.Models;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.Handlers.Handlers
{
    public class MyMessageQueueMessageHandler : AbstractQueueMessageHandler<MyQueueMessage>
    {
        public override Task Handle(MessageContext<MyQueueMessage> messageContext, CancellationToken token)
        {
            Console.WriteLine("MyMessageQueueMessageHandle " + messageContext.Message.SomeString);

            return Task.FromResult(0);
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