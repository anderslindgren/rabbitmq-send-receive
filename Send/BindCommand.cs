namespace RabbitREPL
{
    internal class BindCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "bind exhanges and queues";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}