Simple Service Bus is a simple generic wrapper around Azures message bus. It provides a simple typed interface and allows you to write clean simple hanlders with minimal configuration.

Simply setup your send bus
```csharp
//Setup routing
var routes = new MessageRouteTable()
    .AddRoute<MyQueueMessage>("my-message-queue")
    .AddRoute<MyTopicMessage>("my-message-topic");
    
//Initialise your bus
//Easily wire in to your IOC
ISendOnlyBus bus = new SendOnlyBus(new BusConfigutration(), routes);

//Send those messages!
await bus.Send(new MyQueueMessage { SomeString = "From my queue!" });
await bus.Publish(new MyTopicMessage { SomeString = "From my topic!" });
```

Simply setup your receive bus
```csharp
//Basic endpoint configuration
var defaultHandlerConfig = new HandlerConfiguration {MaxConcurrentCalls = 10};

//Initialise your receive bus
IRecieveOnlyBus bus = RecieveOnlyBus.Initialise(new BusConfigutration())
    .WithDependencyRegistrations(x =>
    {
        //Register your handlers
        x.RegisterQueueHandler<MyQueueMessage, MyMessageQueueMessageHandler>();
        x.RegisterTopicHandler<MyTopicMessage, MyMessageTopicMessageHandler>();
    })
    //Where do you want messages to go?
    .RegisterQueue<MyQueueMessage>("my-message-queue", defaultHandlerConfig)
    .RegisterTopic<MyTopicMessage>("my-message-topic", defaultHandlerConfig);

//On end
bus.Dispose();
```

Create some messages
```csharp
//Queue
public class MyQueueMessage : IQueueMessage
{
    public string SomeString { get; set; }

    public DateTime When { get; set; } = DateTime.UtcNow;
}

//Topic
public class MyTopicMessage : ITopicMessage
{
    public string SomeString { get; set; }

    public DateTime When { get; set; } = DateTime.UtcNow;
}
```

And create your handlers
```csharp
//Queue
public class MyMessageQueueMessageHandler : AbstractQueueMessageHandler<MyQueueMessage>
{
    public override Task Handle(MessageContext<MyQueueMessage> messageContext, CancellationToken token)
    {
        Console.WriteLine("MyMessageQueueMessageHandle " + messageContext.Message.SomeString);

        return Task.FromResult(0);
    }
}

//Topic
public class MyMessageTopicMessageHandler : AbstractTopicMessageHandler<MyTopicMessage>
{
    private readonly ISendOnlyBus bus;

    public override string Subscription => "my-subscription";

    //ISendOnlyBus is auto wired up with the IOC
    public MyMessageTopicMessageHandler(ISendOnlyBus bus)
    {
        this.bus = bus;
    }

    public override async Task Handle(MessageContext<MyTopicMessage> messageContext, CancellationToken token)
    {
        Console.WriteLine("MyMessageTopicMessageHandle " + messageContext.Message.SomeString);

        await bus.Send(new MyQueueMessage {SomeString = " Resent!!"});
    }
}
```


Eezy Peezy!!


