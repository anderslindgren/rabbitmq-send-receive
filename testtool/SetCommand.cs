namespace RabbitREPL
{
    internal class SetCommand : ICommand
    {
        public string Description =>
            "Set properties and permissions";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public SetCommand(Context context, string[] args)
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