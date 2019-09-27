namespace RabbitREPL
{
    internal class PurgeCommand : ICommand
    {
        public string Description =>
            "Purge the queue";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public PurgeCommand(Context context, string[] args)
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