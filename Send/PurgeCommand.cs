namespace RabbitREPL
{
    internal class PurgeCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description => 
            "Purge the queue";
        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}