namespace RabbitREPL
{
    internal class RemoveCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "Removes user or vhost";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}