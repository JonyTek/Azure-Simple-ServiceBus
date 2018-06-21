using System.Threading.Tasks;
using Azure.ServiceBus.Messages;

namespace Azure.ServiceBus.Bus
{
    public interface ISendOnlyBus
    {
        Task Send<T>(T send) where T : IQueueMessage;
        Task Publish<T>(T send) where T : ITopicMessage;
    }
}