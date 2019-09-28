namespace RabbitREPL
{
    class TestCommand : ICommand
    {
        public string Description => "Switches to Test Mode";

        public string DetailedDescription => 
            @"In test mode it is possible to send and recieve messages and use the AMQP protocol";

        private string[] Args { get; set; }
        private Context Context { get; set; }

        public TestCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            Context.TestMode();
        }
    }
}
