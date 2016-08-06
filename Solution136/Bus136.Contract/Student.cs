namespace Bus136.Contract
{
    using NServiceBus;

    public class Student : IMessage
    {
        public string Id { get; set; }

        public string Command { get; set; }

        // you may add whatever properties you need for your assignment
    }
}
