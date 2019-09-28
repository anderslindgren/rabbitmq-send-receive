namespace RabbitREPL
{
    class AdminCommand : ICommand
    {
        public string Description => "";

        public string DetailedDescription => @"";

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
