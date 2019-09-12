using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitREPL
{
    internal class SendCommand : ICommand
    {
        public string Description =>
            "Send a message";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public SendCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }


        public void Execute()
        {
            var channel = Context.Channel;
            var qs = channel.QueueDeclare(queue: "hello-from-ehg", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //var qs = channel.QueueDeclarePassive("hello-from-ehg");
            Console.WriteLine("{0} {1} {2}", qs.QueueName, qs.MessageCount, qs.ConsumerCount);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            for (int i = 0; i < 10; i++)
            {
                channel.BasicPublish(exchange: "", routingKey: "hello-from-ehg", basicProperties: null, body: body);
                Console.WriteLine(" [{0}]\tSent {1}", i, message);
            }
        }
    }
}