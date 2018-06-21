namespace Azure.ServiceBus.Handlers
{
    public abstract class AbstractTopicMessageHandler<T> : AbstractMessageHandler<T>, ITopicMessageHandler<T>
    {
        public abstract string Subscription { get; }
    }
}