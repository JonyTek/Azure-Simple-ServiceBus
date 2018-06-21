using Azure.ServiceBus.Configuration;

namespace Azure.ServiceBus.Client
{
    public class BusConfigutration : IBusConfigutration
    {
        public string ConnectionString =>
            "Endpoint=sb://testing-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=H12YYzUpzC5IVGe3FtyCKpfRRx6XdAQB4VlkbzezAQw=";
    }
}