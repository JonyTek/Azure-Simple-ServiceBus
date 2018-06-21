using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace Azure.ServiceBus.Handlers
{
    public interface IHandler<T>
    {
        Task Handle<TClient>(Message message, TClient client, CancellationToken token)
            where TClient : IReceiverClient;
    }
}