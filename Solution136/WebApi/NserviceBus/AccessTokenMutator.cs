namespace WebApi.NserviceBus
{
    using System;
    using NServiceBus;
    using NServiceBus.MessageMutator;

    public class AccessTokenMutator : IMutateOutgoingTransportMessages, INeedInitialization
    {
        public void MutateOutgoing(NServiceBus.Unicast.Messages.LogicalMessage logicalMessage, TransportMessage transportMessage)
        {
            // this is used for authentication. Normally, this would be some type of encrypted key and hash value
            transportMessage.Headers["access_token"] = DateTime.Now.ToShortDateString();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(c => c.ConfigureComponent<AccessTokenMutator>(DependencyLifecycle.InstancePerCall));
        }
    }
}