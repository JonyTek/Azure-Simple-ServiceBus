using System;

namespace Azure.ServiceBus.Messages
{
    public class MyTopicMessage : ITopicMessage
    {
        public string SomeString { get; set; }

        public DateTime When { get; set; } = DateTime.UtcNow;
    }
}