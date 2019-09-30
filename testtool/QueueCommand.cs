namespace RabbitREPL
{
    internal class QueueCommand : ICommand
    {
        public string Description =>
            "Declare a Queue";
        public string DetailedDescription =>
            @"Declare a Queue

Syntax:
  queue create <queueName> <durable> <exclusive> <autoDelete>
  queue delete <queueName>
  queue purge <queueName>";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public QueueCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            string command = Args[0];
            string queueName = Args[1];

            if (command.Equals("create"))
            {
                // TODO: Check number of arguments and add default values
                bool durable = bool.Parse(Args[2]);
                bool exclusive = bool.Parse(Args[3]);
                bool autoDelete = bool.Parse(Args[4]);
                Context.Channel.QueueDeclare(
                    queue: queueName, 
                    durable: durable, 
                    exclusive: exclusive, 
                    autoDelete: autoDelete, 
                    arguments: null);
            }
            else if (command.Equals("delete"))
            {
                // TODO: Check number of arguments and add default values
                bool ifUnused = bool.Parse(Args[2]);
                bool ifEmpty = bool.Parse(Args[3]);
                Context.Channel.QueueDelete(queue: queueName, ifUnused: ifUnused, ifEmpty: ifEmpty);
            }
            else if (command.Equals("purge"))
            {
                Context.Channel.QueuePurge(queue: queueName);
            }
        }
    }
}