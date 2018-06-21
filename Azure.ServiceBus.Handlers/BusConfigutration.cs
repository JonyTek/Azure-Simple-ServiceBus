using Azure.ServiceBus.Configuration;

namespace Azure.ServiceBus.Handlers
{
    public class BusConfigutration : IBusConfigutration
    {
        public string ConnectionString => "enter-conn-string-here";
    }
}