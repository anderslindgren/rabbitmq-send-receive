using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitREPL
{
    internal class ReceiveCommand : ICommand
    {
        public string Description =>
            "Send a message";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ReceiveCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }


        public void Execute()
        {
            var channel = Context.Channel;
            channel.QueueDeclarePassive(queue: "hello-from-ehg");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("  >>> Received {0}", message);
            };

            channel.BasicConsume(queue: "hello-from-ehg", autoAck: true, consumer: consumer);
        }
    }
}