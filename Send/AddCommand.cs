namespace RabbitREPL
{
    internal class AddCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }

        public string Description => 
            "Add a User";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}