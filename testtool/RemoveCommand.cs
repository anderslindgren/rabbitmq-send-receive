namespace RabbitREPL
{
    internal class RemoveCommand : ICommand
    {
        public string Description =>
            "Removes user or vhost";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public RemoveCommand(Context context, string[] args)
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