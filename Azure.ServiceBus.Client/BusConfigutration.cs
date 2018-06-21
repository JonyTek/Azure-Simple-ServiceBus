using Azure.ServiceBus.Configuration;

namespace Azure.ServiceBus.Client
{
    public class BusConfigutration : IBusConfigutration
    {
        public string ConnectionString => "enter-conn-string-here";
    }
}