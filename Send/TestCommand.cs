namespace RabbitREPL
{
    internal class TestCommand : ICommand
    {
        public string Description =>
            "run some built in tests (alive or health)";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public TestCommand(Context context, string[] args)
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