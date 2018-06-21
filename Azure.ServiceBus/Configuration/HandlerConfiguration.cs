using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus
{
    public class HandlerConfiguration
    {
        public bool AutoComplete { get; set; } = true;
        public int MaxConcurrentCalls { get; set; } = 1;
        public TimeSpan? MaxAutoRenewDuration { get; set; } = null;

        public Func<ExceptionReceivedEventArgs, Task> OnException = args => Task.CompletedTask;

        public MessageHandlerOptions Options()
        {
            return new MessageHandlerOptions(OnException)
            {
                MaxConcurrentCalls = MaxConcurrentCalls,
                AutoComplete = AutoComplete,
                MaxAutoRenewDuration = MaxAutoRenewDuration ?? TimeSpan.FromSeconds(10)
            };
        }
    }
}