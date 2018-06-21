using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.Models
{
    public class MessageContext<T>
    {
        public T Message { get; set; }
        public Message Context { get; set; }
    }
}