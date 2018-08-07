namespace RabbitREPL
{
    internal class SendCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description => 
            "Send a message";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}