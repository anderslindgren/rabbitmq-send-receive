namespace RabbitREPL
{
    internal class WhoAmICommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "Gives the systems view of who you are.";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}