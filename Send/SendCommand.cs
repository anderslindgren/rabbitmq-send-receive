namespace RabbitREPL
{
    internal class SendCommand : ICommand
    {
        public string Description =>
            "Send a message";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public SendCommand(Context context, string[] args)
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