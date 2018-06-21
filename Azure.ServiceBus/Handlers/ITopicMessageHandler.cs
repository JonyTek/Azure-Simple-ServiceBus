using System.Threading;
using System.Threading.Tasks;
using Azure.ServiceBus.Models;

namespace Azure.ServiceBus.Handlers
{
    public interface ITopicMessageHandler<T> : IHandler<T>
    {
        string Subscription { get; }
        Task Handle(MessageContext<T> messageContext, CancellationToken token);
    }
}