using Azure.ServiceBus.Handlers;
using StructureMap;
using StructureMap.Pipeline;

namespace Azure.ServiceBus
{
    public class DependencyRegistration
    {
        public ConfigurationExpression Expression;

        public DependencyRegistration(ConfigurationExpression expression)
        {
            Expression = expression;
        }
        public SmartInstance<THandler, ITopicMessageHandler<TMessage>> RegisterTopicHandler<TMessage, THandler>()
            where THandler : ITopicMessageHandler<TMessage>
        {
            return Expression.For<ITopicMessageHandler<TMessage>>().Use<THandler>();
        }

        public SmartInstance<THandler, IQueueMessageHandler<TMessage>> RegisterQueueHandler<TMessage, THandler>()
            where THandler : IQueueMessageHandler<TMessage>
        {
            return Expression.For<IQueueMessageHandler<TMessage>>().Use<THandler>();
        }

        public SmartInstance<THandler, TInterface> Register<TInterface, THandler>()
            where THandler : TInterface
        {
            return Expression.For<TInterface>().Use<THandler>();
        }
    }
}