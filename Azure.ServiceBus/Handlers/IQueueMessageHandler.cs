using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Handlers
{
    public interface IQueueMessageHandler<T> : IHandler<T>
    {
        Task Handle(MessageContext<T> messageContext, CancellationToken token);
    }
}