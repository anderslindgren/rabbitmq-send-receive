namespace RabbitREPL
{
    internal class QueueCommand : ICommand
    {
        public string Description =>
            "bind exhanges and queues";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public QueueCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}