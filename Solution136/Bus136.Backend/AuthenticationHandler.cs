using System;
using NServiceBus;

namespace Bus136.Backend
{
    public class AuthenticationHandler : IHandleMessages<IMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            var token = Bus.GetMessageHeader(message, "access_token");

            // the token could be anything we want it to be, an encryped-key with hash-value based on date.
            // for now, we are just going to use today's date.
            if (token != DateTime.Now.ToShortDateString())
            {
                Console.WriteLine("User not authenticated");
                Bus.DoNotContinueDispatchingCurrentMessageToHandlers();
                return;
            }

            Console.WriteLine("User authenticated");
        }
    }
}
