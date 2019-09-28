namespace RabbitREPL
{
    internal class QueueCommand : ICommand
    {
        public string Description =>
            "Declare a Queue";
        public string DetailedDescription =>
            @"Declare a Queue

Syntax:
  queue <queueName> <durable> <exclusive> <autoDelete>";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public QueueCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            string queueName = Args[0];
            bool durable = bool.Parse(Args[1]);
            bool exclusive = bool.Parse(Args[2]);
            bool autoDelete = bool.Parse(Args[3]);
            Context.Channel.QueueDeclare(
                queue: queueName, 
                durable: durable, 
                exclusive: exclusive, 
                autoDelete: autoDelete, 
                arguments: null);
        }
    }
}