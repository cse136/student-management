namespace NserviceBus.Backend
{
    using Bus136.Backend;

    using NServiceBus;

    /// <summary>
    /// This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
    /// can be found here: http://particular.net/articles/the-nservicebus-host
    /// </summary>
    [NServiceBus.EndpointName("NserviceBusDemo.Backend")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, ISpecifyMessageHandlerOrdering
    {
        public void Customize(BusConfiguration configuration)
        {
            // NServiceBus provides the following durable storage options
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();

            // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
            // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus

            // Also note that you can mix and match storages to fit you specific needs. 
            // http://docs.particular.net/nservicebus/persistence-order
            configuration.UsePersistence<InMemoryPersistence>();
        }

        public void SpecifyOrder(Order order)
        {
            // we are telling the end point configuration to intercept the messages and call the "AuthenticationHandler" first.
            order.SpecifyFirst<AuthenticationHandler>();
        }
    }
}
