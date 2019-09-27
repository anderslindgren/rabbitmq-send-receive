namespace RabbitREPL
{
    internal class AddCommand : ICommand
    {
        public string Description =>
            "Add a User";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public AddCommand(Context context, string[] args)
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