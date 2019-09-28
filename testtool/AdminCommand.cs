namespace RabbitREPL
{
    class AdminCommand : ICommand
    {
        public string Description => "Switches to Administration Mode";

        public string DetailedDescription => @"In Administration Mode you will use the REST API.";

        private string[] Args { get; set; }
        private Context Context { get; set; }

        public AdminCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            Context.AdminMode();
        }
    }
}
