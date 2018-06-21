namespace Azure.ServiceBus.Handlers
{
    public abstract class AbstractQueueMessageHandler<T> : AbstractMessageHandler<T>, IQueueMessageHandler<T>
    {
    }
}