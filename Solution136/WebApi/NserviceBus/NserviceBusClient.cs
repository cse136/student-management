namespace WebApi.NserviceBus
{
    using NServiceBus;

    public class NserviceBusClient
    {
        public static ISendOnlyBus Bus { get; set; }

        static NserviceBusClient()
        {
            var busConfiguration = new BusConfiguration();

            // if the queue doesn't exist, it'll be created. Make sure to "refresh" in the 
            // computer > manage > services and applications > message queuing > private queues.
            busConfiguration.EndpointName("NserviceBusDemo.Backend");

            // If you use the following endpoint, you will see the messages queued up in Outgoing Queues instead.
            // busConfiguration.EndpointName("NserviceBusDemo.Backend@NoWhere");

            Bus = NServiceBus.Bus.CreateSendOnly(busConfiguration);
        }
    }
}