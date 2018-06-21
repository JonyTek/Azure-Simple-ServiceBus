using System;

namespace Azure.ServiceBus.Bus
{
    public interface IRecieveOnlyBus : IDisposable
    {
        IRecieveOnlyBus RegisterQueue<T>(string route, HandlerConfiguration config = null);
        IRecieveOnlyBus RegisterTopic<T>(string route, HandlerConfiguration config = null);
        IRecieveOnlyBus WithDependencyRegistrations(Action<DependencyRegistration> configure);
    }
}