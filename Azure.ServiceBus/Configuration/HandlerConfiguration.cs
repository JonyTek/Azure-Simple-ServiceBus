using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.Configuration
{
    public class HandlerConfiguration
    {
        public bool AutoComplete { get; set; } = true;
        public int MaxConcurrentCalls { get; set; } = 1;
        public TimeSpan? MaxAutoRenewDuration { get; set; } = null;

        internal MessageHandlerOptions Options(Func<ExceptionReceivedEventArgs, Task> onException)
        {
            return new MessageHandlerOptions(onException)
            {
                MaxConcurrentCalls = MaxConcurrentCalls,
                AutoComplete = AutoComplete,
                MaxAutoRenewDuration = MaxAutoRenewDuration ?? TimeSpan.FromSeconds(1)
            };
        }
    }
}