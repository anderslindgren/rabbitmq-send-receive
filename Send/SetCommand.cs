namespace RabbitREPL
{
    internal class SetCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "Set properties and permissions";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}