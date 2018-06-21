using System;

namespace Azure.ServiceBus.Messages
{
    public class MyQueueMessage : IQueueMessage
    {
        public string SomeString { get; set; }

        public DateTime When { get; set; } = DateTime.UtcNow;
    }
}