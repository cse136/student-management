namespace Bus136.Backend.Handlers
{
    using System;
    using Bus136.Contract;
    using NServiceBus;

    public class StudentHandler : IHandleMessages<Student>
    {
        public void Handle(Student message)
        {
            Console.Write("Student ID received: " + message.Id + "\n");
            Console.Write("Student Command received: " + message.Command + "\n");

            // 136 TODO: you can now handle any request accordingly...

            // Uncomment out the following line if you want to simulate a down-time on the handler (say the database is down)
            // the retry queue will hold the message
            // throw new Exception("database down");
        }
    }
}
